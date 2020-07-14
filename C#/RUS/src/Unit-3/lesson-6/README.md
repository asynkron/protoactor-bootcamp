# Урок 6: Рефакторинг с использованием механизма переключения поведения.

На этом уроке мы с вами добавим к нашему приложению новую функциональность. Данная функциональность будет заключаться в том что, мы будем эмулировать запуск и остановку воспроизведения фильма пользователем. Для реализации этой функциональности мы будем использовать поведение.

Первое что нам нужно сделать, это создать новое сообщение, под названием `StopMovieMessage()`. Данное сообщение будет сигнализировать нашему актору о том, что пользователь решил остановить просмотр фильма. 

После этого нам нужно создать новый актор, который будет непосредственно отвечать за запуск и остановку воспроизведения фильма. Назовём его `UserActor()`.

Прежде всего, добавим конструктор нашему актору. В конструкторе мы создаём экземпляр класса `Behavior()` и инициализируем его поведением по умолчанию.

```c#
public UserActor()
{
    Console.WriteLine("Creating a UserActor");
    ColorConsole.WriteLineCyan("Setting initial behavior to stopped");
    _behavior = new Behavior(Stopped);
}
```

Теперь создадим два метода для представления двух различных типов поведения. Поведение для воспроизведения фильма и поведение для остановки воспроизведения фильма. Следует иметь в виду что, любой метод который будет отвечать за поведение актора, должен реализовывать определённый делегат. 

```c#
public delegate Task Receive(IContext context);
```

Первое что нам нужно сделать, это добавить приватный метод `Playing()` в наш актор. Данный метод будет, представлять собой поведение для воспроизведения фильма. 

```c#
private Task Playing(IContext context)
{
    switch (context.Message)
    {
        case PlayMovieMessage msg:
            ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping existing one");
            break;
         case StopMovieMessage msg:
            ColorConsole.WriteLineYellow($"User has stopped watching '{_currentlyWatching}'");
            _currentlyWatching = null;
            _behavior.Become(Stopped);
            break;
    }
    ColorConsole.WriteLineCyan("UserActor has now become Playing");

    return Actor.Done;
}
```

Как мы видим, после получения сообщения наш метод с помощью оператора `switch` выбирает подходящую бизнес логику для обработки сообщения. Давайте более подробно разберём обработку сообщения `StopMovieMessage`. Как вы видите, после обработки основной бизнес логики идёт переключение поведения на `Stopped`. 

`_behavior.Become(Stopped);`

Это означает, что следующее сообщение поступит на обработку не в метод `Playing()`, а в метод `Stopped()`. Напомним, что метод `Stopped()` служит для реализации поведения остановки просмотра фильма. Давайте рассмотрим его более подробно.

```c#
private Task Stopped(IContext context)
{
    switch (context.Message)
    {
        case PlayMovieMessage msg:
            _currentlyWatching = msg.MovieTitle;
            ColorConsole.WriteLineYellow($"User is currently watching '{_currentlyWatching}'");
            _behavior.Become(Playing);
            break;
        case StopMovieMessage msg:
            ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing");
            break;
     }
     ColorConsole.WriteLineCyan("UserActor has now become Stopped");

     return Actor.Done;
}
```

Как мы видим в блоке кода отвечающего за обработку сообщения `PlayMovieMessage()` присутствует код для переключения поведения на поведение `Playing` в свою очередь поведение `Playing`, при определённых условиях переключится на поведения `Stopped`. То есть наш актор представляет собой классический конечный автомат с двумя состояниями. 

В итоге наш актор должен выглядеть следующим образом.

```c#
public class UserActor : IActor
{
    private string _currentlyWatching;

    private readonly Behavior _behavior;

    public UserActor()
    {
        Console.WriteLine("Creating a UserActor");
        ColorConsole.WriteLineCyan("Setting initial behavior to stopped");
        _behavior = new Behavior(Stopped);
    }

    public Task ReceiveAsync(IContext context) => _behavior.ReceiveAsync(context);

    private Task Stopped(IContext context)
    {
        switch (context.Message)
        {
            case PlayMovieMessage msg:
                _currentlyWatching = msg.MovieTitle;
                ColorConsole.WriteLineYellow($"User is currently watching '{_currentlyWatching}'");
                _behavior.Become(Playing);
                break;
            case StopMovieMessage msg:
                ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing");
                break;
        }
        ColorConsole.WriteLineCyan("UserActor has now become Stopped");

        return Actor.Done;
    }

    private Task Playing(IContext context)
    {
        switch (context.Message)
        {
            case PlayMovieMessage msg:
                ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping existing one");
                break;
            case StopMovieMessage msg:
                ColorConsole.WriteLineYellow($"User has stopped watching '{_currentlyWatching}'");
                _currentlyWatching = null;
                _behavior.Become(Stopped);
                break;
        }
        ColorConsole.WriteLineCyan("UserActor has now become Playing");

        return Actor.Done;
    }
}
```

Теперь все на что нам обходимо сделать это изменить класс `Program()` чтобы он отправлял нам необходимые сообщения.

```c#
class Program
{
    static void Main(string[] args)
    {
        var system = new ActorSystem();
        Console.WriteLine("Actor system created");

        var props = Props.FromProducer(() => new UserActor());
        var pid = system.Root.Spawn(props);


        Console.ReadKey();
        Console.WriteLine("Sending PlayMovieMessage (The Movie)");
        system.Root.Send(pid, new PlayMovieMessage("The Movie", 44));
        Console.ReadKey();
        Console.WriteLine("Sending another PlayMovieMessage (The Movie 2)");
        system.Root.Send(pid, new PlayMovieMessage("The Movie 2", 54));

        Console.ReadKey();
        Console.WriteLine("Sending a StopMovieMessage");
        system.Root.Send(pid, new StopMovieMessage());

        Console.ReadKey();
        Console.WriteLine("Sending another StopMovieMessage");
        system.Root.Send(pid, new StopMovieMessage());

        Console.ReadLine();
     }
}
```

Давайте запустим наш проект и посмотрим что у нас получилось.

![](images/3_6_1.png)