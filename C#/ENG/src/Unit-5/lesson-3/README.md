# Lesson 3: Pool Router.

In the previous lesson, we looked at what types of routers are available in the Proto.Actor platform. These routers can be divided into three types: a router where the business logic is in your actor, an actor group router, and an actor pool router. 

Let's look at routers based on the actor pool. When using a pool, you are not required to create or manage routes, the router will do so. You can use the pool when all routes are created and used in the same way, and there is no need to reconstruct routes specifically. In other words, for 'simple' routes, a pool is a good choice.

#### Router creation.

Let's look at router creation using the example of the BroadcastPool.

```c#
private static readonly Props MyActorProps = Props.FromProducer(() => new MyActor());

var system = new ActorSystem();
var context = new RootContext(system);
var props = context.NewBroadcastPool(MyActorProps, 5);
var pid = context.Spawn(props);
for (var i = 0; i < 10; i++)
{
    context.Send(pid, new Message { Text = $"{i % 4}" });
}
```

As you can see, creating a router is almost no different than creating a normal actor.

First of all, we create an actor system in which our router will be located, then we create a context and using the context method `NewBroadcastPool(MyActorProps, 5);` we create an instance of our router.

The `NewBroadcastPool(MyActorProps, 5)` method accepts Props describing our actor and as the second parameter, the number of instances of our actor.

After we create our router in the context.spawn(props) method; we will be able to send messages to it using the method

`context.Send(pid, new Message { Text = $"{i % 4}" });`. 

And the router, in turn, will forward this message to five copies of our actor.

Usually, messages sent to the router are sent to the routes. However, some messages are processed by the router itself. An example of such messages is the system message `Stop()` . This message will not be forwarded to the routes, but will be processed by the router itself. In this case, the router will be suspended, and since it is a pool, all routes that are actually its child actors will be suspended along with it.

As we already know, when we send a message to a router, it will only be sent to one route, at least that's what most routers do. But it is possible to force a router to send a message to all routes. To do this, we can use another special broadcast message: RouterBroadcastMessage(). When the router receives this message, it will forward the content to all routes. Broadcast messages `RouterBroadcastMessage()` can be sent to routers of both types - pools and groups.

#### Remote Routes.

Previously in this article, the role of routes was played by local actors, but note that routers are capable of sending messages to actors on different servers. It is not difficult to create a route that works on a remote server.

For this purpose, the Proto.Actor platform has a special service messages that allows you to add or remove a route to an existing router. You can also use these messages to add local actors if necessary. Let us look at the message in more detail.

- RouterAddRoutee - This message is used to add a new route to the router. It accepts the PID of the local or remote actor and adds it to the routing table.
- RouterRemoveRoutee - Unlike the previous message, this message removes the actor PID from the routing table.
- RouterGetRoutees - To request the routing table by the router used this message.
- Routees - The message contains the current status of our router's routing table.

#### Observation.

Another router feature that you should talk about is observation. Because a router creates routes, it is also the supervisor for these actors. 

The default router always transmits service messages to its supervisor. This can lead to unexpected results. When one of the routes crashes, the router will report it to its supervisor. This supervisor will almost certainly want to restart the route, but will restart the router instead of the route. 

And restarting the router will restart all routes, not only the emergency ones. From the outside, it looks as if the router uses the AllForOneStrategy strategy. To solve this problem, you can customize your strategy when the router is created.

When one of the routes fails, only it will be restarted, and all others will continue to work as if nothing had happened. 

In this lesson, you learned how flexible pools could be. For example, you can change the number of routes and even the routing logic. And when there are multiple servers, you can easily create routes on different servers. 

But sometimes the restrictions imposed by pools are too strict, and there is a desire to have more freedom in creating and managing routes. In this situation, you can use groups.