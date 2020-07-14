# Lesson 6: Guaranteed delivery.

A guaranteed delivery channel is a point-to-point channel that guarantees that the message will be delivered to the recipient. It means that the delivery will be performed even if any error occurs. A channel must have a variety of mechanisms and checks to guarantee delivery; for example, the message must be stored on disk in case the system crashes. 

Do you always need a guaranteed delivery channel when creating systems? Is it possible to create a high-reliability system without guaranteeing the delivery of messages in the system? Yes, you need certain guarantees, but maximum availability is not always required.

Implementation of a guaranteed delivery channel cannot guarantee delivery in all possible situations, such as when an error occurs when sending a message. In such a situation, it is impossible to send a message because it disappeared with the sender. You must constantly ask yourself the question: is the level of guarantee sufficient for my purposes?

When creating a system, you need to know which guarantees the channel provides, and are there enough these guarantees for your system. Let's see which guarantees the Proto.Actor platform offers for you.

The main rule of message delivery - the message must be delivered no more than once. This means that Proto.Actor promises to deliver the message only once or not deliver it at all. This quality is not particularly good for reliable systems. Why isn't Proto.Actor implemented with a full delivery guarantee? The first reason is that a full delivery guarantee is complex and requires a lot of overhead. This impairs performance even when you don't need a full shipping guarantee.

Secondly, no one needs just reliable delivery. We need to know if the request has been processed, but this requires sending a confirmation message. What can not be implemented at the Proto.Actor platform level. And the last reason why Proto.Actor doesn't implement a fully guaranteed delivery is because, on top of the basic guarantees, you can always impose more stringent guarantees. But the opposite is impossible: you can't make a strict system less strict without no kernel changes.

The Proto.Actor platform does not guarantee delivery of the message. In fact, no system can provide such a guarantee. But Proto.Actor implements the basic rule of message delivery to local and remote actors. Looking at these two situations separately, you can see that Proto.Actor is not as bad as it might seem.

Sending local messages in General almost does not fail, because this is a normal method call. The failure can only be caused by something catastrophic that happened in the depths of the system, such as **StackOverflowError**, **OutOfMemoryError**, or a violation of memory access rights. In all these cases, the actor will not be physically able to process the message. Therefore, the guarantees of message delivery to local actors are quite high.

Message loss can really be a problem when using remote actors. Failure to deliver messages to remote actors is more likely, especially if they are separated by unreliable networks. If someone pulls out an Ethernet cable or turns off the router power, the message will be lost. To solve this problem, Proto.Actor uses the **gRPC** library.

** gRPC** is a high-performance framework developed by Google for remote procedure call (RPC), running on top of HTTP/2.

gRPC is easy to use, great for creating distributed systems (microservices) and API. It has built-in support for load balancing, tracing, authentication, and service viability checks. It is possible to create client libraries for working with the backend in 10 languages. High performance is achieved through the use of HTTP/2 and Protocol Buffers.