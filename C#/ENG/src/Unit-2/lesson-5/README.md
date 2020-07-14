# Lesson 5: Actor Instantiation.

After we've learned the basic concepts, let's consider how we can create our first actor, and then create an instance of it in our actor system.

So, in this lesson, we'll start by creating an actor. We will inherit it from the IActor interface. After we define our actor, we will create a Props class to configure it. And then, using Props, we'll create an instance of our actor in the actor system. After that, we'll see how to get a link to our newly created actor.

So the first thing we need to do is create a new class, let's name it PlaybackActor. Next, add a link to the Proto namespace, and then inherit our class from the IActor interface. After that, we need to implement the ReceiveAsync method from the IActor interface. The result is that our class should look like this.

```c#
using System;
using System.Threading.Tasks;
using Proto;

public class PlaybackActor : IActor
{
    public PlaybackActor() => Console.WriteLine("Creating a PlaybackActor");
  
    public Task ReceiveAsync(IContext context)
    {
        return Actor.Done;
    }
}
```

Please note that all the business logic of our actor will be located in the ReceiveAsync method.

For the actor system to know that the actor has worked without any errors, you should return `Actor.Done` . At the end of our actor's work.

After we have implemented the basis of our future actor, we will need to create an actor system where an instance of our actor will work. You can do this by creating an instance of the `ActorSystem()` class.

```c#
var system = new ActorSystem();
```

Note that you should create a single instance of the `Actor System () ' class for the entire application in most cases.

Next, we need to create a Props class to set the parameters for creating an instance of our actor.

Since in this example, we do not need to make any special settings. The process of creating a Props class will be straightforward. We will call the factory method `Props.FromProducer`. And we will pass on the actor as the argument of the method.

```c#
var props = Props.FromProducer(() => new PlaybackActor());
```

After we created the props class, which contains a description of the actor creation process, we can create an instance of our actor and get the link to it.

For this, we will use the factory method Spawn.

```c#
var pid = system.Root.Spawn(props);
```

As we can see here, all we need to do to create an instance of our actor and get a link to it is to pass the props class to the factory method Spawn.

At the end of the lesson, our code should look like this. 

```c#
using System;
using System.Threading.Tasks;
using Proto;

namespace MovieStreaming
{
    public class PlaybackActor : IActor
    {
        public PlaybackActor() => Console.WriteLine("Creating a PlaybackActor");
        public Task ReceiveAsync(IContext context)
        {
            return Actor.Done;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var system = new ActorSystem();

            Console.WriteLine("Actor system created");

            var props = Props.FromProducer(() => new PlaybackActor());
            var pid = system.Root.Spawn(props);
            Console.ReadLine();
        }
    }
}
```

If you don't succeed, there is a ready-made solution in the lesson folder. You can always run it and compare it with yours.

In the next lesson, we will add business logic to our actor.