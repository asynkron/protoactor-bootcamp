# Lesson 3: Snapshots.

When developing real-world applications, you may find that some actors will generate too many events. This, in turn, can lead to a very long event log, which will lead to long recovery time for the actor. 

Sometimes a correct approach may be to divide an actor into several actors. However, if this solution is not acceptable, you can use snapshots of the state to reduce the actor's recovery time significantly.

Persistent actors usually save a snapshot of the state every n events. Or when the specified condition will be met.

When the condition for saving a snapshot is triggered, processing of incoming messages is paused until the snapshot is saved. This means that the state can be safely stored.

During a state restore, the last saved state snapshot is used for initialization. Once the status snapshot has been uploaded, it starts loading and processing events that occur after the last saved status snapshot. To restore the actor to its current (i.e. last) state.

#### Deleting a snapshot.

To free space in the storage, the Persistent actor can delete old snapshots based on the index of that snapshot. All you need to do is call the `DeleteSnapshotsAsync()` method and pass the unnecessary state's index.

#### Deleting an event.

Usually in applications, deleting events is not used at all, or it is used in combination with snapshots. Since by deleting events, you lose the history of how the state of the actor changed before it reached the current state. However, if you need to delete events for some reason, you can use the `DeleteEventsAsync () ' method and pass the index of the required event.

#### Practical implementation.

Let's look at how we can use state snapshots in our actor system in practice. To do this, let's rewrite a little bit of the example from the last lesson so that it can to support working with state snapshots. We need to change the constructor of our calculator by replacing the call of the `WithEventSourcing()` method with the `WithEventSourcingAndSnapshotting()` method.

The difference between these methods is that the `WithEventSourcingAndSnapshotting()` method saves events and takes periodic state snapshots according to the specified predicate.

```c#
public Calculator(IProvider provider)
{
    _persistence = Persistence.WithEventSourcingAndSnapshotting(
    provider,
    provider,
    "demo-app-id",
    ApplyEvent,
    ApplySnapshot,
    new IntervalStrategy(2), () => _result);
}
```

We will also need to add the ApplySnapshot()method to our class so that our actor can restore its state from the snapshot.

```c#
private void ApplySnapshot(Snapshot snapshot)
{
    switch (snapshot)
    {
        case RecoverSnapshot msg:
            if (msg.State is double ss)
            {
                _result = ss;
                Console.WriteLine("MyPersistenceActor - RecoverSnapshot = Snapshot.Index = {0}, Snapshot.State = {1}", _persistence.Index, ss);
            }
            break;
    }
}
```

