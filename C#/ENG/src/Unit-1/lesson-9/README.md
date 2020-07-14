# Lesson 9: What're Props, RootContext, and ActorContext in Proto.Actor.

So, after we discovered what actors and messages are. Let's take a look at the context of the actor.

Our actors do not exist by themselves. They need a certain context. Context is the infrastructure required to create and run actors.

In Proto.Actor, there are two types of context: RootContext and ActorContext.

### RootContext

It used for the initial creation of the actor. And also, RootContext is used for interaction with actors in our system. 

### ActorContext

Unlike RootContext, which created in a single instance for the entire application, ActorContext is created individually for each actor. Like RootContext, ActorContext is used to generate child actors and interact with other actors. But ActorContext also stores a lot of additional information that is necessary for a particular actor instance to function, such as a list of the child actors of our actor or a message that we have received for processing.

We will review the methods contained in RootContext and ActorContext in more detail during our course. 

### Props

This is a configuration class that allows you to set parameters for creating actors. Includes a lot of related information about creating an actor, for example. Which Task Manager to use or which mailbox to use, etc.