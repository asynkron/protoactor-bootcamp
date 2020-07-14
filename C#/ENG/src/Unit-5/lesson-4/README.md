# Lesson 4: Group Router.

In the previous lesson, we looked at pools, a distinctive feature of pools is that they, themselves, manage routes. When using routers with groups, you will have to create your routes. This can be useful when you need to have more control over when and how routes are created. 

In this lesson, we will first create a simple router for the group. Then we will consider how we can dynamically change the routes using set of router messages.

#### Group creation.

Groups are created almost like pools. The only difference is that when creating a pool, you need to specify the number of route instances, and when creating a group, you need to specify a list of routes.

```c#
var system = new ActorSystem();
var context = new RootContext(system);
var props = context.NewBroadcastGroup(
    context.Spawn(MyActorProps),
    context.Spawn(MyActorProps),
    context.Spawn(MyActorProps),
    context.Spawn(MyActorProps)
);
for (var i = 0; i < 10; i++)
{
    var pid = context.Spawn(props);
    context.Send(pid, new Message { Text = $"{i % 4}" });
}
```

As you can see, setting up a group is almost the same as setting up a pool. Creating a group looks even easier than setting up a pool because there is no need to define how routes should be created. And since the group definition uses actor paths, you don't need to change anything to add remote actors, just add the full path to the remote actor:

A router with a group is used almost like a pool. The only difference is that it completes the route. When the route entering the pool is terminated, the router detects this and removes it from the pool. A router for a group does not support this feature. When the route is terminated, the router will continue to send it messages because the router does not control routes, and perhaps at some point in the future the actor will be available.