# Proto.Actor Bootcamp - Module 6: Message channels.

<img src="images/protowhite.png" alt="protowhite" style="float: left; zoom: 20%;" />

## Concepts you'll learn.

In this module, we will consider the channels that can be used to transfer messages between actors. At first, we will get acquainted with two types of channels: "point-to-point" and "publisher/subscriber". We have used point-to-point channels in all previous examples in this course, but sometimes we need a more flexible way to send messages to recipients. 

The section on publisher/subscriber channels describes a method for sending messages to multiple recipients that do not require the sender to know who the message is addressed to in advance. Recipients are connected to the channel and can change as the app runs. For channels of this type, EventQueue or EventBus are often used. The Proto.Actor platform has an `EventStream () ' class that implements publisher/subscriber channels.

Next in this module, two special types of channels will be presented. The first one - the undelivered messages channel - stores the messages that were not delivered. Sometimes it is also called a queue of undelivered messages. This channel can help you debug situations where some messages are not being processed for some unknown reason, or to monitor some issues during execution.

The last section describes the type of guaranteed delivery channels. You can't create a reliable system without at least minimal guarantees of message delivery. But strict delivery guarantees are not always necessary. The Proto.Actor platform does not provide a full delivery guarantee, but we will describe the level that is supported, which differs for local and remote actors.

## Table of Contents

1. [Channels Types.](lesson-1/README.md)
2. [Point-to-point Channel.](lesson-2/README.md)
3. [Publisher/Subscriber Channel.](lesson-3/README.md)
4. [EventStream.](lesson-4/README.md)
5. [DeadLetter Channel.](lesson-5/README.md)
6. [Guaranteed delivery.](lesson-6/README.md)

