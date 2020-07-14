# Lesson 2: Load balancing with Proto.Actor routers.

One of the main reasons to use routers is to provide even load distribution between multiple actors to improve the system's performance that handles large number of messages. 

These can be local actors (vertical scaling) or actors on remote servers (horizontal scaling), with the routing support built into the Proto.Actor platform, you can easily implement scaling support in your application.

In our previous example with a road camera, it takes a relatively long time for a license plate to be recognized. To organize parallel recognition of multiple license plate numbers, we will use a router.

![](images/5_2_1.png)

In the figure below, you can see that the router is capable of sending messages to one of the LicenseRecognition actor instances. When a message hits the router, the router selects one of the available actors and sends it to that actor. When it receives the next message, the router chooses another actor and sends it to that actor.

To implement this router, we will use built-in routing support in the Proto.Actor platform. In Proto.Actor there is a division between the router that contains the routing logic and the actor representing the router. 

The routing logic decides which route to choose and can be used within the actor. A router actor is a stand-alone actor that downloads the routing logic and other settings from the configuration and can control the routes themselves.

The Proto.Actor platform has two kinds of built-in routers.

#### Pool.

In the pool, routers are responsible for creating actors and removing them from the list when they are finished. The pool can be used when all actors are created and distributed equally, and there is no need for special procedures to create actors.

#### Group.

In the group, routers do not control the creation of actors. Actors are created by the system and the router uses the actor selection operation. A router with a group does not control the actors. Actors must be managed somewhere else in the system. A group can be used when you need to manage actors' life cycle in a special way or when you need more control over where and when actors are created.

The pool is simpler because it supports automatic actor management, but you have to pay for this simplicity by not being able to customize individual actors' logic.

![](images/5_2_2.png)

In the picture, you can see the difference between a pool and a group. When you use a pool, actors are descendants of a router, and when you use a router with a group, actors can be descendants of any other actor (in this example, the ActorCreator actor). Actors do not necessarily have to be descended from the same parent. They just have to be created and run.

The Proto.Actor platform has several built-in routers. Let's look at them in more detail.

- Round-robin — sequential sending of messages to the actors.
- Hash key — key addressing in the message.
- Random — the recipient is selected at random.
- Weighted round robin — the same as Round-robin, but you can adjust the frequency of sending messages to specific recipients using weighting factors.
- Broadcast — the message is sent to all the actors in the group.