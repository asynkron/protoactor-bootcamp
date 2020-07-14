# Lesson 2: Persistence actors.

Persistence actor operates in two modes: processes commands or restores the last known state from the event history.  Commands are messages sent to the actor to execute some logic; events are the confirmation that the actor has executed the logic without errors. The first thing we will do is define commands and events for the calculator actor. The listing includes the commands that the calculator can execute and the events that occur after the acceptable commands are checked by the calculator.

```protobuf
syntax = "proto3";
package messages;
option csharp_namespace = "Messages";

message AddCommand {
	double value = 1;
}

message SubtractCommand {
	double value = 1;
}

message DivideCommand {
	double value = 1;
}

message MultiplyCommand {
	double value = 1;
}

message PrintResultCommand {}

message ClearCommand {}

message ResetEvent {}

message AddedEvent {
	double value = 1;
}

message SubtractedEvent {
	double value = 1;
}

message DividedEvent {
	double value = 1;
}

message MultipliedEvent {
	double value = 1;
}
```

Now, after we have defined the commands and events we need to add the `Persistence()` class to our actor. This class will be responsible for saving and replaying events.

```c#
public Calculator(IProvider provider)
{
    _persistence = Persistence.WithEventSourcing(
    provider,
    "demo-app-id",
    ApplyEvent);
}
```

Then we add the ability to process commands to the `ReceiveAsync` method.

```c#
public async Task ReceiveAsync(IContext context)
{
    switch (context.Message)
    {
        case Started msg:
            Console.WriteLine("MyPersistenceActor - Started");
            await _persistence.RecoverStateAsync();
            break;

        case AddCommand msg:
            await _persistence.PersistEventAsync(new AddedEvent { Value = msg.Value });
            _result += msg.Value;
            break;

        case SubtractCommand msg:
            await _persistence.PersistEventAsync(new SubtractedEvent { Value = msg.Value });
            _result -= msg.Value;
            break;

        case DivideCommand msg:
            await _persistence.PersistEventAsync(new DividedEvent { Value = msg.Value });
            _result /= msg.Value;
            break;

        case MultiplyCommand msg:
            await _persistence.PersistEventAsync(new MultipliedEvent { Value = msg.Value });
            _result *= msg.Value;
            break;

        case ClearCommand msg:
            await _persistence.PersistEventAsync(new ResetEvent());
            _result = 0;
            break;

        case PrintResultCommand msg:
            Console.WriteLine(_result);
            break;
    }
}
```

As you can see, each command's implementation simply performs the required mathematical action and saves the result to the _result variable. The difference between our actors and the previous ones created in this training course is that we create an event for each mathematical action and then save it using the `persistence.PersistEventAsync` method.

In order to be able to restore state our actor must call method `await _persistence.RecoverStateAsync();` in message handler Started.

To restore the state of the actor using events. The actor must contain an appropriate method for processing them.

```c#
private void ApplyEvent(Event @event)
{
    switch (@event)
    {
        case RecoverEvent msg:
            if (msg.Data is AddedEvent addedEvent)
            {
                _result += addedEvent.Value;
            }
            else if (msg.Data is SubtractedEvent subtractedEvent)
            {
                _result -= subtractedEvent.Value;
            }
            else if (msg.Data is DividedEvent dividedEvent)
            {
                 _result /= dividedEvent.Value;
            }
            else if (msg.Data is MultipliedEvent multipliedEvent)
            {
                 _result *= multipliedEvent.Value;
            }
            else if (msg.Data is ResetEvent resetEvent)
            {
                 _result = 0;
            }
            break;
        case ReplayEvent msg:
            break;
        case PersistedEvent msg:
            break;
    }
}
```

Once the event we need is extracted from the database, it will be passed to the ApplyEvent method for further processing to recreate the internal state of our actor.

After we've looked at the key points, let's see how we can create an actor system using Persistence and a suitable date provider. As we mentioned earlier, all events that have occurred must be stored. For this purpose, the Proto.Actor platform provides several built-in data providers for different databases. As well as an interface to implement your own data provider. 

Let's see how we can use the built-in Proto.Actor provider for SQL Lite database in our actor system.

```c#
static void Main(string[] args)
{
    var system = new ActorSystem();
    var context = new RootContext(system);
    var provider = new SqliteProvider(new SqliteConnectionStringBuilder { DataSource = "states.db" });

    var props = Props.FromProducer(() => new Calculator(provider));
    var pid = context.Spawn(props);

    system.Root.Send(pid, new AddCommand {Value = 100});
    system.Root.Send(pid, new SubtractCommand { Value = 50 });

    system.Root.Send(pid, new PrintResultCommand ());

    system.Root.Poison(pid);

    pid = context.Spawn(props);

    system.Root.Send(pid, new PrintResultCommand());
    Console.ReadLine();
}
```

As you can see from the above code, the only difference from the previous examples in this course is to create a provider `SqliteProvider()` of its configuration and pass it to the actor with support of Persistence.

```c#
var provider = new SqliteProvider(new SqliteConnectionStringBuilder { DataSource = "states.db" });
var props = Props.FromProducer(() => new Calculator(provider));
```

And so let's sum up. Each command in our calculator actor becomes an event and is logged, and then a new calculation result is written to the `_result` variable.