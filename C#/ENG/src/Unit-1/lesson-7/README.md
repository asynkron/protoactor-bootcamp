# Lesson 7: What's an actor in Proto.Actor.

In this lesson, we will learn more about the main features of actors. And so let's get started. First of all, I would like to note that the actor model is well-described by one phrase. **Everything is an actor**.

It means that the actors are the fundamental, primitive computing units that do all the work in our system or other words, the actors are the building blocks of our system.

In an application, actors must perform small, well-defined tasks, so our application consists of multiple actors, each of which performs a strictly defined task ([Single-responsibility principle](https://en.wikipedia.org/wiki/Single-responsibility_principle)). 

It means that creating an application is reduced to dividing the task into small subtasks (Decomposition) and implementing these subtasks as separate actors.

Thanks to the Proto.Actor platform, the actor code looks the same regardless of whether it is local or remote. In this way, the actor will behave equally, whether it is running on a local or remote machine. 

When you create an actor, you don't get a direct link to it. Instead, You get a PID (short for process ID) - a serializable ID that is used to send messages to the actor. The advantage of this is that PID's can be easily serialized and forwarded via messages, allowing actors to communicate remotely.

Sending messages is a critical element in the actors' work. Actors do not directly invoke the methods of another actor. Instead, they send a message to the other actor with the instruction to execute a command. This allows them to achieve loose coupling in the system.

Actors are pretty lazy things. They just sit there and do nothing. Unless we send them a message, so if we don't send them any messages, they just sit and wait for some message to come in so they can get to work.

By and large, there are four basic things an actor can do.

1. Receive and react to messages.
2. Create more actors.
3. Send messages to other actors.
4. Change the state to process the next message.

We will see examples of all these things throughout the course.

To make it more clear. What is an actor? Let's look at what an actor consists of:

1. State.
2. Behavior.
3. Mailbox.
4. Child actors.
5. Supervisor strategy.

**State**

Actor usually contain fields that represent the state of the actor. This data is the actor's value, and it must be protected from direct influence by other actors.

One of the concepts of actors in Proto.Actor is that there is no direct reference to an instance of an actor class - it is impossible to call the actor method. The only way to interact between actors is to communicate using asynchronous messages, which will be described below.

A significant advantage of actors when developing reactive systems - there is no need to synchronize access to the actor using locks because they are de facto asynchronous. Consequently, the developer writes the logic of the actor's work without worrying about parallelism problems at all.

Since the state is critical to the actor's actions, the presence of an uncoordinated state is fatal. Thus, if an actor fails and the supervisor restarts it, the actor state will be reset to its original state as it was when the actor was first created. It solves the problem of the actor's fault tolerance by allowing the system to self-recover.

However, it is possible to configure the actor to automatically restore to the state before restarting.

**Behavior**

Behavior means a function that determines the actions to be performed in response to a message, such as forwarding a request if the client is authorized, rejecting, etc.

This behavior can change over time, for example, because different clients are authorized overtime, or because the actor can go into "no service" mode. The behavior changes depending on changes in state variables that are taken into account in the logic of the actor’s work, and the function itself can be changed at runtime.

When the actor is restarted, its behavior is reset to the initial state. However, with the help of configurations, it is possible to automatically restore the behavior of the actor to the state before the restart.

**Mailbox**

The purpose of an actor is to process messages sent from other actors inside the system or messages received from external systems. The element that connects the sender and recipient is the actor's mailbox: each actor has exactly one mailbox in which the senders place their messages. Because actor instances can run on multiple threads simultaneously, it may seem that messages do not have a specific processing order, but this is not the case. Sending various messages from the same source to a particular actor will put them in the queue in the same order.

There are different ways to implement mailboxes. By default, this is FIFO: the order of messages processed by the actor corresponds to the order in which they were queued. In most cases, this is the best choice, but applications may need to prioritize some messages over others.

The algorithm for placing messages in the mailbox can be configured and queued depending on the priority of messages or using another custom algorithm. When using such a queue, the order of processed messages will naturally be determined by the queue algorithm and not be FIFO.

An important feature in which Proto. Actor differs from other implementations of the actor model because the actor's current behavior must always process the next message from the queue. However, when configuring out of the box, validation (whether the actor with the current behavior can process the received message) does not occur. Failure to process a message is usually considered a failure.

**Child Actors**
Each actor is potentially a supervisor: if it creates child actors to delegate subtasks, it automatically controls them. The list of child actors supported in the actor context (it should be noted that descendants in the second generation are "grandchildren", not directly accessible). Changes in the list are made by creating (`context.actorOf (...)`) or stopping (`context.stop (child)`) child actors. Actions to create and terminate child actors have performed asynchronously, so they do not block their supervisor.

**Supervisor strategy**
The last part of the actor is the strategy for handling failures in child actors. The standard strategies in the case of a failure in the child actor are:

- to resume the work of the subordinate actor, to keep its status;
- to resume the work of the subordinate actor, restore its standard state;
- to stop work of a subordinate actor;
- pass the failure up (not recommended, used in exceptional situations).

ince this strategy is crucial for the actor and its child actors, it cannot be changed after the actor created.

Given that there is only one such strategy for each actor, if it is necessary to apply different strategies for subordinate actors, they grouped under intermediate supervisor actors with the corresponding strategies. According to the division of tasks to subtasks., it is good practice to structure actor systems once again.

Proto.Actor is asynchronous, non-blocking, and supports message exchange. It is scalable vertically as well as horizontally. To support fault tolerance, it has tracking mechanisms. It meets all the requirements for creating reactive systems.