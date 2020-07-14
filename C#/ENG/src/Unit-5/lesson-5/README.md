# Lesson 5. ConsistentHashing Router

As you have seen in previous lessons, routers are a great way to create vertical and horizontal scalable systems. But all of the routers that we looked at earlier relied on a random route to process the message. But sometimes we need to send messages with common properties on the same route.

For example, you can create a key value repository that will store your data in a database. In this case, each message's common property will be the key that identifies the item in the database.

Let's go back to our example with the traffic camera. We can present each car as a separate actor and use the car number as a key. In this way, our router will send a message with a fine only to the right actor. 

This way, we can take into account how many times during the day the car exceeded the speed limit, and then, at the end of the day, we can decide to issue a fine or revoke the driver's license.

To solve this problem, using traditional routers, we would have to resort to a routing table with constant updating of the routing table. However, this approach requires a lot of coordination to update the routing table when adding new values. Ideally, we would need a router with no state retention that would allow us to select a specific route based on a message.

To do this, the router must identify the messages as similar. To do this, ConsistentHashing calculates the hash code for the message and maps it to one of the routes. The mapping process consists of several steps, as shown below.

![](images/5_5_1.png)

**Step 1** Converts the message into a message key object. Similar messages, for example, with the same identification number, will get the same keys. The key type is absolutely unimportant; the only limitation is that for similar messages, you always have to get the same keys objects. The keys for messages with different types must be different. In the Ptoto.Actor platform, the `IHashable()` interface, is used to convert the message into a key.

**Step 2** Creates a hash code based on the message key. This hash code is used to select the virtual host **(step 3)**, and in the last step (4)**, a specific route is selected to process all messages corresponding to this virtual host. 

The first thing that catches your eye is using a virtual host. Couldn't you immediately determine the route by hash code? Virtual hosts are used to get a more even distribution of messages between routes. The number of virtual nodes served by a route is configured using the replicaCount. 

Creating an instance of the ConsistentHashing router is no different from creating other routers on the Proto.Actor platform.

```c#
var system = new ActorSystem();
var context = new RootContext(system);
var props = context.NewConsistentHashPool(MyActorProps, 5);
var pid = context.Spawn(props);
for (var i = 0; i < 10; i++)
{
    context.Send(pid, new Message { Text = $"{i % 4}" });
}
```









