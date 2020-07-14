# Lesson 6: Defining Which Messages an Actor will processing.

In the previous lesson, we created an actor system, and an instance of our actor inside that system. In this lesson, we will learn how to send and respond to messages within the actor system.

For this, we modify our PlaybackActor to respond to messages like string and int types. Next, we will use the Send method to send a message to our actor.

So, let's go to the source code of our application and see how we can send a couple of messages to our actor. To do this, we will need the link to our actor that we received in the previous lesson and the `Send` method that our actor system provides.

```c#
var pid = system.Root.Spawn(props);

system.Root.Send(pid, "The Movie");
system.Root.Send(pid, 44);
```

As you can see, we just sent two messages to our actor. The first message is a string containing the title of the movie, and the second message contains a digital user ID that represents int type. 

If we run our applications now, nothing will happen because the `ReceiveAsync` method of the `PlaybackActor` actor does not contain business logic. 

So let's go to the `ReceiveAsync` method and implement the processing of our messages. First of all, we need to determine what type of message has come to us for processing. To do this, we need to extract the message from the `context.Message` property and pass it to the switch operator. For selecting an appropriate logic for message processing. 

```c#
public Task ReceiveAsync(IContext context)
{
    switch (context.Message)
    {
        case string movieTitle:
            break;
        case int userId:
            break;
    }
    return Actor.Done;
}
```

Now let's implement the business logic. Our business logic will be very simple. We will just output the content of the message to the console.

```c#
public Task ReceiveAsync(IContext context)
{
    switch (context.Message)
    {
        case string movieTitle:
            Console.WriteLine($"Received movie title {movieTitle}");
            break;
        case int userId:
            Console.WriteLine($"Received user ID {userId}");
            break;
    }
    return Actor.Done;
}
```

If we launch our app now, we will see that the movie name and user id are displayed on the console.

![](images/2_7_1.png)

What if we want to combine the movie title and user ID? There are custom messages for this purpose, and we will look at them in more detail in the next lesson.