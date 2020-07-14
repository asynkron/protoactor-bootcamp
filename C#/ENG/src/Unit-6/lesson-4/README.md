# Lesson 4: EventStream.

In the Proto.Actor platform the EventStream class implements the support of "publisher/subscriber" channels. Each ActorSystem has such an object available to any actor (by system.EventStream). EventStream can be considered as a multi-channel publisher/subscriber dispatcher because any actor can subscribe to messages of a certain type. When some other actor publishes a message of this type, the subscriber will receive it.

You can subscribe anywhere in the system where links to EventStream are available. To subscribe the actor to receive messages, you should invoke the Subscribe method of EventStream class. And to save the value returned by it. This value contains a full description of the just made subscription.

```c#
orderSubscription = Cluster.System.EventStream.Subscribe<Order>(msg => Console.WriteLine("message received"))
```

When the subscription is no longer necessary, for example, when the gift campaign is over, you can use `Unsubscribe` method. In this example, we have canceled the subscription of the GiftModule component, and after calling this method, the actor will stop receiving Order messages.

```c#
orderSubscription.Unsubscribe()
```

It is all that is required to subscribe GiftModule component to receive Order messages. After calling the `Subscribe` method, the GiftModule component will be receiving all Order messages published in EventStream. This method can be invoked for any actor that is interested in receiving Order messages. And when an actor needs to receive messages of different types, the `Subscribe` method can be called several times with varying kinds of messages.

Publishing messages in EventStream is as easy as that; just call the Publish method, 

```c#
system.EventStream.Publish(msg);
```

After that, the msg message will be passed to all subscribed actors. In fact, this completes description of the realization of channels like "publisher/subscriber" in Proto.Actor.

In Proto.Actor you can subscribe to several types of events at once. For example, our GiftModule component must also handle order cancellation messages, because in this case, the gift should not be sent. To do this, the GiftModule component must subscribe to EventStream to receive Order and Cancel messages. Both subscriptions are independent, i.e. after the cancellation of the Orders subscription, the Cancel message subscription will continue to be valid, and these messages will be delivered.

Earlier we mentioned the advantages of loose coupling between sender and receiver, and the dynamic character of the "publisher/subscriber" channel. And since EventStream is available to all actors, it is also an excellent solution for cases when all messages from all over the local system must flow into one or more actors. Bright examples are the logging. All logs should flock to a single point and be written to a log file.