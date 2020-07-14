# Lesson 1: Why use the actor model

So, why should we use the actor model in our application? Using an actor model makes it easy to create scalable, competitive, high throughput, and low latency applications.

Therefore, if you need to create an application that has one or more of these properties, an actor model may be the right choice for you.

So let's go deeper into why we may need to use the actor model and Proto.Actor platform in our application.

### No more manual threads control.

First of all, if we create a competitive application using the actor model, we no longer need to control threads manually. And we can be sure that our code is thread-safe. Now instead of using locks for shared resources, we use the actor model so that we don't have to do this error-prone and complicated work manually.

### High level of abstraction.

We get a high level of abstraction using the actor model since everything in our application will be considered as actors.

When we want that one actor to interact with another actor, we do it by sending messages. You can treat the actor model as object-oriented programming on steroids. Because the actor model is formalizing interaction between objects by sending messages.

### Vertical scalability (scale-up).

Since the actor model controls the scalability for us, we can quickly increase the computational power on which our application is running. For example. If we add additional processors or RAM to our server, the actor model automatically scale-up and use additional resources without changing the source code of the application. Also, we do not need to manage threads and locks manually.

### Horizontal scalability (scale-out).

Thanks to the actor model in addition to vertical scalability, where we add computational resources to the local server, we can use horizontal scalability, where we distribute our task to multiple physical servers.

### Fault tolerance and error handling.

When we use the actor model in our application, we get the benefit of fault tolerance and error handling out of the box. 

In this course, we will see how Proto.Actor isolates errors in a subset of the actor hierarchy, and if there are arise errors that can't be handled, we can restart these actors to create a self-recovery system.

### Common Architecture.

Finally. When we are using an actor model to represent our system. We speak a common language. So instead of talking about a lot of different ways to do the same things within an app, we have to learn a way to do these things using the actor model and stick to them.
