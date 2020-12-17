# Lesson 3: Actor's address and PID.

In the previous lesson, we looked at the diagram where `UserActor()` sends a message `IncrementPlayCount()` to the actor `MoviePlayCounterActor()`. But as you can see, the actor `MoviePlayCounterActor()` is not a child actor `UserActor()` so we will not have a direct link to the actor `MoviePlayCounterActor()`. So what should we do in this case?![](images/4_3_1.png)

I want to remind you that in the Proto.Actor platform, the reference to another actor is PID. PID, in turn, is an abbreviation for process ID, i.e., it is a unique process identifier in the actor system.

The PID consists of a combination of an address and an ID. The address is the address of the host where the actor is hosted. for a remote host, it can be the IP address of the machine where the actor instance is hosted. The ID is a unique identifier of the actor on this host. It consists of a unique number and the name of the actor written in the format.

```c#
$"{System.ProcessRegistry.NextId()}/{name}"
```

Thus, knowing the address and ID of the interested actor, we can create its PID manually. It is done in the following way.

```c#
var moviePlayCounterActorPid = new PID(system.ProcessRegistry.Address, "$1/MoviePlayCounterActor");
```

Now we can use the resulting PID's to send messages to our actor. Regardless of where it is deployed and whether our actor is a child.
