# Proto.Actor Bootcamp - Module 9: Persistence Actor.

<img src="images/protowhite.png" alt="protowhite" style="float: left; zoom: 20%;" />

## Concepts you'll learn.

The actor state stored in RAM is lost when the actor is stopped or restarted or when the entire actor system is stopped or restarted. In this chapter, we will describe how to ensure that this state is preserved using the Proto.Persistence module.

Often, databases are used to create, read, modify, and delete records (these operations are also called CRUD operations). Database records are often used to store the current state of the system. In this case, the database acts as a container.

To organize state storage in Proto.Actor, a technology called Event Sourcing, is used, and the first section of this module will be dedicated to it. You will learn how to store state changes as a sequence of unchangeable events in the database log.

Next, we will investigate the Persistent Actor. The persistent actor makes it easy to record its state in the form of events and restore it after an error or restart.

The Proto.Cluster and Proto.Persistence modules can be used together to create cluster applications that continue to work normally even after a crash or multiple nodes are replaced.

But first, let's take a look at Event Sourcing, the technology behind Proto.Persistence.

#### When might I need to restore an actor's state?

Note at once that it makes no sense to make each actor stored just because it is possible. When should the actor's state be preserved? This largely depends on the requirements of the system and the interaction of other systems with actors.

Actor whose life cycle lasts longer than it takes to process a single message and accumulates valuable information over time often requires long-term state storage. Actors of this kind have an id through which such an actor can be accessed later. Examples are the actor of the shopping cart; the user goes between the sections of the web store in search of the necessary products, adds products to the cart, and removes them from there. It would be wrong to show an empty cart after an unexpected crash or restart of the actor. Another case is when many systems send messages to the actor who simulates a message matching machine between systems. The actor can memorize the messages received and construct some context for the surrounding systems. Examples are a system that receives orders from a website and coordinates the process of receiving products from the warehouse, delivery, and accounting by integrating them with several existing services. In the case of a restart, the actor must continue to operate from the last known correct state so that the connected systems can continue to operate in the required order.

These are just a few examples when it is necessary to preserve the state of the actor. It is unnecessary to try to preserve the state if the actor performs all the work from start to finish while processing a single message. Examples include the actor who processes HTTP queries that do not have a state and contain all the necessary information. In this case, if a failure occurs, the client can repeat the HTTP request.

## Table of Contents

1. [What is Event Sourcing.](lesson-1/README.md)
2. [Persistence actors.](lesson-2/README.md)
3. [Snapshotting.](lesson-3/README.md)

