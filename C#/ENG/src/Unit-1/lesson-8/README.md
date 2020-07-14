# Lesson 8: What's a message in Proto.Actor.

So, now we understand a lot more about actors. Let's talk about messages.

The message is a simple class that allows you to describe the subject area. The class of message must not be inherited from any parent class.

An instance of the message class must be unchangeable. When you create an instance of the message class, you must make sure that it won't be modified in the future.

In most cases, sending messages is an asynchronous operation. This means that the actor sends a message to another actor and, not waiting for the other actor to process its message, continues to do his work.

This is what the reactive programming manifesto says about messages. A message is a data element that sent to a specific address. In a message-based system, recipients wait for and react to messages, otherwise, they are in a waiting state. So, here we see another confirmation of how lazy are actors. They will just doze until we send them a message.

Let's look at an example of what might look like a message class written in C#.

```c#
private class ExampleMessage
{
    public int CustomerID { get; }

    public Hello(int customerId)
    {
        CustomerID = customerId;
    }
}
```



We see that we have a simple class. We do not need to inherit from the base class or implement any special interfaces. 

Note that the ExampleMessage class has a constructor that takes the value customerId. When we create an instance of the ExampleMessage class, the constructor stores the passed value in the CustomerID property. But since the CustomerID property is read-only, no one else can modify it, and we don't have to worry that someone will destroy our business logic by changing its value.