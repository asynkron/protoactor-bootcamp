# Lesson 5: Key features of the Proto.Actor.

So, when we got a basic understanding of the actor model and Proto.Actor. Let's begin to consider the key features of the Proto.Actor platform.

Proto.Actor offers us:

- Simple multithreading and asynchronous (concurrency and asynchronous)
- Simple distributed processing (distribution)
- High performance
- Fault Tolerance
- Ability to use multiple programming languages in a single application (multiple languages)

### Concurrency and asynchronous.

Thanks to the fact that we work with high levels of abstraction such as actors, messages, and Finite-state machines. We work with parallelism at high level of abstraction, so we do not need to perform low-level operations such as manual control, threads, and blocking shared resources. Because message passing is an asynchronous operation, we get the ease of interaction between actors running in parallel. This avoids blocking the actor while waiting for a response from another actor.

### Distributed processing.

The concept of location transparency, which we will focus on in more detail, essentially means that it does not matter where the individual actor instances are. They can be on one machine or another computer. 

Proto.Actor also provides a simple remote deployment model in which we use the configuration to specify where we want our actor to be created.

### High performance.

Proto.Actor has high performance; although the performance will depend a lot on the machine on which we run it, you can expect to handle about 50 million messages per second per machine,. In terms of overheads on actor management. We can expect to be able to create about 2.5 million instances of actors per gigabyte of memory.

In addition, Photo.Actor has additional features, such as load balancing and routing messages to multiple child instances of the actor.

### Fault Tolerance.

Proto.Actor allows us to create self-repairing systems. This is done using the concept of a supervisor hierarchy. This concept is that if an error occurs in any part of the system, we isolate it from the rest of the system and tell the system  how to make self-recover.

### Ability to use multiple programming languages in a single application.

A key feature of Proto.Actor is the ability to write a single application in multiple programming languages. Thanks to the use of gRPC, actors written in different programming languages can easily exchange messages with each other. For example. You can write app, split this app into client and server parts. Write the client part on ASP.NET Core, and the server part in Python. And communication between the server and client parts will be absolutely transparent for you.