# Lesson 5: Creating MoviePlayCounterActor.

As in the previous lesson, we will need to do some preparatory work. First of all, we need to create `PlaybackStatisticsActor()` the goal of this class is to create a child class `MoviePlayCounterActor()` and save a link to it.

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

Next, we need to add a new message `IncrementPlayCountMessage()` which is used to notify the `MoviePlayCounterActor () ' actor about which movie was started.

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

Now we need to edit the "Stopped" behavior of the `UserActor () 'actor so that it sends the message' IncrementPlayCountMessage ()' to 'MoviePlayCounterActor ()' immediately after starting the movie.

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

After this preparatory work, we can finally begin developing the actor `MoviePlayCounterActor()`.

To do this, add a new `MoviePlayCounterActor ()` class to the Actors folder.

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

As you can see, the logic of this actor is straightforward. After receiving the `IncrementPlayCountMessage()` message, the corresponding movie is searched in `_moviePlayCounts` dictionary, and if there is one, the counter of this movie is increased by one.

Let's run our application and look at the result.