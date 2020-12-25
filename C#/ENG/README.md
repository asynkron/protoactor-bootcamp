# Proto.Actor Bootcamp

<img src="images/protowhite.png" alt="protowhite" style="float: left; zoom: 20%;" />

Welcome to  [Proto.Actor](http://proto.actor/) Bootcamp. It is a free course for self-study.

This training course consists of nine main parts, during which you will learn how to create fully functional, real-world programs using Proto.Actor  actors and many other components of the Proto.Actor Framework.

We will start with the basics of actors and gradually approach more complex examples.

This course is for self-study — you can do it at any pace you like.

### What will you learn from this course

In this course, you will learn how to use Proto.Actor Flamework to create reactive, parallel systems. You will learn how to develop applications that seemed impossible or very complicated before learning Proto.Actor Flamework. After this course, you will feel more confident in solving large and complex tasks.

### Module 1: Introduction to Actor Model and Proto.Actor

In the first module, we will give the basic definitions of the actor model and the Proto.Actor Framework:

1. [Why use the actor model.](src/Unit-1/lesson-1/README.md)
2. [Types of applications for which actors are suitable.](src/Unit-1/lesson-2/README.md)
3. [Use of Proto.Actor in different types of applications.](src/Unit-1/lesson-3/README.md)
4. [The Reactive Manifesto](src/Unit-1/lesson-4/README.md)
5. [Key features of the Proto.Actor.](src/Unit-1/lesson-5/README.md)
6. [Actors and messages.](src/Unit-1/lesson-6/README.md)
7. [What's an actor in Proto.Actor.](src/Unit-1/lesson-7/README.md)
8. [What's a message in Proto.Actor.](src/Unit-1/lesson-8/README.md)
9. [What're Props, RootContext, and ActorContext in Proto.Actor.](src/Unit-1/lesson-9/README.md)
10. [Overview of the supervisor hierarchy in Proto. Actor.](src/Unit-1/lesson-10/README.md)
11. [Installing Proto.Actor.](src/Unit-1/lesson-11/README.md)

### Module 2 Defining and using actors and messages.

In the previous module, we learned the basic concepts of the actors model and messages. In this chapter, we will study these concepts in more detail. You will learn how to use actors and messages in your applications.:

1. [Defining Actors.](src/Unit-2/lesson-1/README.md)
2. [Actor References.](src/Unit-2/lesson-2/README.md)
3. [Defining Messages.](src/Unit-2/lesson-3/README.md)
4. [Types of Message Sending.](src/Unit-2/lesson-4/README.md)
5. [Actor Instantiation.](src/Unit-2/lesson-5/README.md)
6. [Defining Which Messages an Actor will processing.](src/Unit-2/lesson-6/README.md)
7. [Sending a Custom Message.](src/Unit-2/lesson-7/README.md)

### Module 3 Understanding Actor Lifecycles and states.

From this module, you will learn about the actors' life cycle, and also the internal state of the actor.

1. [Actor Lifecycle.](src/Unit-3/lesson-1/README.md)
2. [Actor Lifecycle Messages.](src/Unit-3/lesson-2/README.md)
3. [Terminating Actors and Hierarchy of Actors](src/Unit-3/lesson-3/README.md)
4. [What is the Poison Pill message and how to work with it..](src/Unit-3/lesson-4/README.md)
5. [Switchable Actor Behavior](src/Unit-3/lesson-5/README.md)
6. [Refactoring with using behavior switching .](src/Unit-3/lesson-6/README.md)

### Module 4 Creating actor hierarchy and error handling.

In this module, you will learn how to create a self-recovering system.

1. [Supervisor and actor hierarchy.](src/Unit-4/lesson-1/README.md)
2. [Overview of the application that demonstrates the supervisor's capabilities and the actors hierarchy.](src/Unit-4/lesson-2/README.md)
5. [Actor's address and PID.](src/Unit-4/lesson-3/README.md)
6. [Creating UserCoordinatorActor.](src/Unit-4/lesson-4/README.md)
7. [Creating MoviePlayCounterActor.](src/Unit-4/lesson-5/README.md)
8. [How parent actors are watching over their children actors.](src/Unit-4/lesson-6/README.md)
9. [Strategies to control the state of children's actors.](src/Unit-4/lesson-7/README.md)

### Module 5 Message Routing

In this part of our course, you will learn how to quickly and easily solve the scalability problem in your application with routers.

1. [Router pattern.](src/Unit-5/lesson-1/README.md)
2. [Load balancing with Proto.Actor routers.](src/Unit-5/lesson-2/README.md)
3. [Pool Router.](src/Unit-5/lesson-3/README.md)
4. [Group Router.](src/Unit-5/lesson-4/README.md)
5. [ConsistentHashing Router.](src/Unit-5/lesson-5/README.md)
6. [Implementation of the router pattern with using actors.](src/Unit-5/lesson-6/README.md)

### Module 6 Message channels

In this module, you will learn about the different types of channels that used to send messages between actors.

1. [Channels Types.](src/Unit-6/lesson-1/README.md)
2. [Point-to-point Channel.](src/Unit-6/lesson-2/README.md)
3. [Publisher/Subscriber Channel.](src/Unit-6/lesson-3/README.md)
4. [EventStream.](src/Unit-6/lesson-4/README.md)
5. [DeadLetter Channel.](src/Unit-6/lesson-5/README.md)
6. [Guaranteed delivery.](src/Unit-6/lesson-6/README.md)

### Module 7 Proto.Actor Remote

Here we will see how to create a distributed application that runs on multiple computers or virtual machines. You'll see how Proto.Actor helps you accomplish this most complex task:

1. [What is horizontal scaling.](src/Unit-7/lesson-1/README.md)
2. [Overview Proto.Actor Remote.](src/Unit-7/lesson-2/README.md)
3. [Example of working with Proto.Actor Remote.](src/Unit-7/lesson-3/README.md)

### Module 8 Proto.Actor Cluster

In module 8, you learned how to create distributed applications with a fixed number of nodes. The static membership approach is simple but does not have a ready-made load balancing solution or fault tolerance. A cluster allows you to dynamically increase and decrease the number of nodes used by a distributed application, eliminating the fear of having a single point of failure:

1. [Why do you need clusters.](src/Unit-8/lesson-1/README.md)
2. [Membership in the cluster..](src/Unit-8/lesson-2/README.md)
3. [Joining to the cluster.](src/Unit-8/lesson-3/README.md)
5. [Processing tasks in the Cluster.](src/Unit-8/lesson-4/README.md)
6. [Running a Cluster.](src/Unit-8/lesson-5/README.md)
7. [How to distribute tasks by using routers.](src/Unit-8/lesson-6/README.md)
8. [Reliable task processing.](src/Unit-8/lesson-7/README.md)
9. [Cluster Testing.](src/Unit-8/lesson-8/README.md)

### Module 9 Persistence Actor

The actor state contained in RAM will be lost when the actor will be stopping or restarting or when the actor system will be stopped or restarted. In this chapter, we will show you how to save this state using the Proto.Actor Persistence module:

1. [What is Event Sourcing.](src/Unit-9/lesson-1/README.md)
5. [Persistence actors.](src/Unit-9/lesson-2/README.md)
8. [Snapshotting.](src/Unit-9/lesson-3/README.md)

## How to get started

Here's how the Proto.Actor Bootcamp works.

### Use Github to Make Life Easy

This Github repository contains Visual Studio solution files and other assets you will need to complete the bootcamp.

Thus, if you want to follow the bootcamp we recommend doing the following:

1. Sign up for [Github](https://github.com/), if you haven't already.
2. [Fork this repository](https://github.com/petabridge/akka-bootcamp/fork) and clone your fork to your local machine.
3. As you go through the project, keep a web browser tab open to the [Proto.Actor Bootcamp](https://github.com/AsynkronIT/protoactor-bootcamp/) so you can read all of the instructions clearly and easily.

#### When you're doing the lessons...

A few things to bear in mind when you're following the step-by-step instructions::

- **Don't just copy and paste the code shown in the lesson's README**. You will memorize and learn all the built-in Proto.Actor functions if you will typewriter this code yourself. [Kinesthetic learning](http://en.wikipedia.org/wiki/Kinesthetic_learning) FTW!
- Don't be afraid to ask questions. You can reach the Proto.Actor team and other Proto.Actor users in our Gitter chat [here](https://gitter.im/AsynkronIT/protoactor).

## Docs

We will provide explanations of all key concepts throughout each lesson, but of course, you should bookmark (and feel free to use!) the [Proto.Actor Docs](http://proto.actor/docs/)

## Tools / prerequisites

This course expects the following:

1. You have some programming experience and familiarity with C#
2. A Github account and basic knowledge of Git.
3. You are using a version of Visual Studio ([it's free now!](http://www.visualstudio.com/))
