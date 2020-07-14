# Lesson 4: Types of Message Sending.

In Proto.Actor there are several ways to send messages. 

The first way is to use the Tell method. This method is used when one actor wants to send a message to another actor and, not waiting to get a response from them, continue their work.

As opposed to the Tell method, Proto.Actor provides the Ask method. This method, after sending a message, suspends the execution of the current actor until a response is received.

And finally, the third method is the Forward method. This method is used when it is necessary to forward a received message to another actor. This method is usually used when you want to send root messages in the system.

So, let's see what the main differences are between the Tell and Ask methods.

- As we mentioned earlier. The Tell method is used when one actor wants to send a message to another actor and continue working without waiting for a response.
- Conversely, the Ask method will send a message to the target actor and then wait for a response message.

Thus, the main difference between these methods is that the Tell method is not waiting for an answer, while the Ask method is waiting. The Tell method can be seen as fire and forget. We will simply send a message and no longer have to worry about it, whereas the Ask method is waiting for a response from the target actor.

When we use the Tell method, the actor is not blocked, for example, the actor will be blocked while waiting for a response from another actor, because no response is expected, so no blocking occurs.

Although the Ask method is also non-blocking and works asynchronously, it can use a timeout to determine that a remote actor is not responding to requests. Accordingly, if the target actor does not respond after the specified time interval expires, we will get an exception to the request.

In general, the Tell method offers better scalability and parallelism in the actor system because we simply send a message and forget about it, without incurring any additional overhead. 

In its turn, the Ask method has worse scalability and parallelism than the Tell method.

This is because behind the scenes, Proto.Actor must create special classes that will track the response in the future, and also Proto.Actor must track the timeout value. These are all additional overheads that the Tell method does not have. 

Using the Tell method can also help to create more loosely coupled systems, whereas using the Ask method, we link the sender and receiver in a bidirectional link, which makes our system more vulnerable.

Therefore, the Tell method will be used most of the time throughout the system. And the Ask method is used only when it is absolutely necessary for system development.