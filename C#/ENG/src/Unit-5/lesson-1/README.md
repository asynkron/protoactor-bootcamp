# Lesson 1: Router pattern.

Let's take a look at what the router pattern is a whole and when and how it applies, and then take a more detailed look at the implementation of each particular router.

We will consider the router pattern on a specific example. This example will be an application for working with traffic cameras. This application will analyze the speed of the car, and if the speed exceeds the set limit, a fine will be issued, but if the speed is below the set limit, the message containing the number and speed of the car will be deleted.

To solve this problem, we will use the "Router" template. As you can see in the figure, the router can send messages to different actors depending on the message content.

![](images/5_1_1.png)

There are many reasons to use logic to select the route you want the message to be sent on. But basically, there are three reasons for organizing message stream control in applications.

#### Performance.

It takes a long time to process a message, but it is possible to process messages in parallel. In other words, messages can be processed by multiple actors in parallel. In the road camera example, cars that are caught on the camera can be processed by multiple parallel actors, because all processing logic is concentrated within the actor.

#### Received messages have different content.

The message has an attribute (as a number plate in our example), depending on the value of which the message must be processed by different actors.

#### Depending on the state of the router.

For example, when the camera is in reserved mode, all messages must be sent to deletion; otherwise, they must be processed as usual.

In all cases (regardless of the reason or specific logic used) the router must decide to which actor to send the message.

In this module, we will consider different approaches to message routing. Along the way, we'll get acquainted with several more mechanisms of Proto.Actor platforms that can be useful not only for implementing routers but also for other processes, such as when necessary to process messages differently depending on the state of the actor. 