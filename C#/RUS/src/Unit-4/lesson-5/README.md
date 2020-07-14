# Урок 5: Создание MoviePlayCounterActor.

Как и в кенгуру, нам потребуется сделать некоторую подготовительную работу. Прежде всего, нам нужно создать класс `PlaybackStatisticsActor()` задача данного класса сводится к тому, что бы создать дочерний класс `MoviePlayCounterActor()` и сохранить на него ссылку.

```c#
public class PlaybackStatisticsActor : IActor
{
    private PID _moviePlayCounterActorRef;

    public Task ReceiveAsync(IContext context)
    {
        switch (context.Message)
        {
            case Started msg:
                var props = Props.FromProducer(() => new MoviePlayCounterActor());
                _moviePlayCounterActorRef = context.Spawn(props);
                break;
        }
        return Actor.Done;
    }
}
```

Далее нам нужно добавить новое сообщение `IncrementPlayCountMessage()` которое служит для оповещения актора `MoviePlayCounterActor()` о том просмотр, какого фильма был запущен.

```c#
public class IncrementPlayCountMessage
{
    public string MovieTitle { get; }

    public IncrementPlayCountMessage(string movieTitle)
    {
        MovieTitle = movieTitle;
    }
}
```

Теперь нам нужно отредактировать поведение Stopped актора `UserActor()` что бы он отправлял сообщение `IncrementPlayCountMessage()` актору `MoviePlayCounterActor()` сразу после запуска просмотра фильма.

```c#
private Task Stopped(IContext context)
{
    switch (context.Message)
    {
        case PlayMovieMessage msg:
            _currentlyWatching = msg.MovieTitle;
            ColorConsole.WriteLineYellow($"User is currently watching '{_currentlyWatching}'");
            context.Send(_moviePlayCounterActorRef, new IncrementPlayCountMessage(_currentlyWatching));
            _behavior.Become(Playing);
            break;
         case StopMovieMessage msg:
            ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing");
            break;
         default:
            ColorConsole.WriteLineCyan("UserActor has now become Stopped");
            break;
    }
    return Actor.Done;
}
```

После всей этой подготовительной работы, мы, наконец, можем приступить к разработке актора `MoviePlayCounterActor()`.

Для этого добавим в папку Actors новый класс `MoviePlayCounterActor()` .

```c#
public class MoviePlayCounterActor : IActor
{
    private Dictionary<string, int> _moviePlayCounts = new Dictionary<string, int>();
    public Task ReceiveAsync(IContext context)
    {
        switch (context.Message)
        {
            case IncrementPlayCountMessage msg:
                ProcessIncrementPlayCountMessage(msg);
                break;
        }
        return Actor.Done;
    }

    private void ProcessIncrementPlayCountMessage(IncrementPlayCountMessage message)
    {
        if (!_moviePlayCounts.ContainsKey(message.MovieTitle))
        {
            _moviePlayCounts.Add(message.MovieTitle, 0);
        }
        _moviePlayCounts[message.MovieTitle]++;

        ColorConsole.WriteMagenta($"MoviePlayerCounterActor '{message.MovieTitle}' has been watched {_moviePlayCounts[message.MovieTitle]} times");
    }
}
```

Как вы видите, логика данного актора предельно проста. После получения сообщения IncrementPlayCountMessage() происходит поиск соответствующего фильма в словаре _moviePlayCounts и если он есть, счётчик просмотров данного фильма увеличивается на единицу.



Давайте запустим наше приложение и посмотрим на результат.