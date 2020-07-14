# Lesson 1: Defining Actors.

Let's start by looking at the steps necessary to create an actor. Let's also look at some of the patterns that you should keep in mind when you create your actor. 

The first thing we need to do is create a new class in our IDE for our actor. This class will define our actor, which will be later used in our actor system. 

Next, we must inherit our class from the Proto.Actor base class. After this Proto.Actor Platform will be able to create and manage instances of our actor.

The last thing we need to do is define what types of messages our actor can be handling, and then write handlers for these message types.

There are a few principles that you should follow when designing actors. The first principle is that when creating the actor should, carefully treat the resources that the actor consumes since our actor will work with many other actors. We should be attentive to how many computing resources our actor uses and how much memory it consumes. Since our actor coexists with many other actors in the system, we do not want it to take over all available computing resources, thereby slowing down our system. 

Ideally, our actors should be functionally related. It means that the actor performs a well-defined task and do it very well.

For example, we don't need one actor that handles authentication, chat notifications, and other actions in one person. Instead, we break down these tasks into separate actors.

We should also make sure that external actors do not change the internal state of our actors. The only way to change the state of our actors is to react to the messages that they receive. Plus, we should strive to place our actors in the actor hierarchy by transferring the most error-prone parts of our application to the lower layers of the hierarchy so that our actor system can self-repairing.

Later in this course, we will have an entire module on hierarchies and how to create hierarchies. 

