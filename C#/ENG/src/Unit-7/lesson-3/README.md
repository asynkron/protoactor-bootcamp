# Lesson 3: Example of working with Proto.Actor Remote.

To understand how to work with **Proto.Remote**, let's create a chat that will use **Proto.Remote** to transfer messages between chat members. This application will consist of three components. This is the server, client, and library of the messages.

#### Library with messages.

We will begin the discussion of our example by looking at the message library. These messages will be used to exchange data between chat members. All we need to do to create a message library is to create a new project and add the following code to it.

```protobuf
syntax = "proto3";
package messages;
option csharp_namespace = "chat.messages";
import "Proto.Actor/Protos.proto";

message Connect
{
	actor.PID Sender = 1;
}

message Connected {
    string Message = 1;
}

message SayRequest {
    string UserName = 1;
    string Message = 2;
}

message SayResponse {
    string UserName = 1;
    string Message = 2;
}

message NickRequest {
    string OldUserName = 1;
    string NewUserName = 2;
}

message NickResponse {
    string OldUserName = 1;
    string NewUserName = 2;
}
```

Thanks to the fact that we use gRPC, clients written in any of the programming languages supported by the Proto.Actor platform can become members of our chat room.

#### Creation of a server

The server is a standard console application that links to our messaging library. 

```c#
class Program
{
    static void Main(string[] args)
    {
        var system = new ActorSystem();
        var serialization = new Serialization();
        var context = new RootContext(system);
        serialization.RegisterFileDescriptor(ChatReflection.Descriptor);
        var remote = new Remote(system, serialization);
        remote.Start("127.0.0.1", 8000);

        var clients = new HashSet<PID>();
        var props = Props.FromFunc(ctx =>
        {
            switch (ctx.Message)
            {
                case Connect connect:
                    Console.WriteLine($"Client {connect.Sender} connected");
                    clients.Add(connect.Sender);
                    ctx.Send(connect.Sender, new Connected { Message = "Welcome!" });
                    break;
                case SayRequest sayRequest:
                    foreach (var client in clients)
                    {
                        ctx.Send(client, new SayResponse
                        {
                            UserName = sayRequest.UserName,
                            Message = sayRequest.Message
                        });
                    }
                    break;
                case NickRequest nickRequest:
                    foreach (var client in clients)
                    {
                        ctx.Send(client, new NickResponse
                        {
                            OldUserName = nickRequest.OldUserName,
                            NewUserName = nickRequest.NewUserName
                        });
                    }
                    break;
            }
            return Actor.Done;
        });

        context.SpawnNamed(props, "chatserver");
        Console.ReadLine();
    }
}
```

As you can see, all we need to do to start receiving messages from the remote actor system is to create an instance of the `Remote()` class and run it by calling the `Start` method.

```c#
serialization.RegisterFileDescriptor(ChatReflection.Descriptor);
var remote = new Remote(system, serialization);
remote.Start("127.0.0.1", 8000);
```

After you run the `Remote()` instance, our server will open the specified port and be ready to accept incoming connections. 

Otherwise, the server actor is no different from standard actors.

#### Creation of a client.

The client also is a console application and refers to a message library.

```c#
class Program
{
    static void Main(string[] args)
    {
        var system = new ActorSystem();
        var serialization = new Serialization();
        serialization.RegisterFileDescriptor(ChatReflection.Descriptor);
        var remote = new Remote(system, serialization);
        remote.Start("127.0.0.1", 0);
        var server = new PID("127.0.0.1:8000", "chatserver");
        var context = new RootContext(system, default, openTracingMiddleware);

        var props = Props.FromFunc(ctx =>
        {
            switch (ctx.Message)
            {
                case Connected connected:
                    Console.WriteLine(connected.Message);
                    break;
                case SayResponse sayResponse:
                    Console.WriteLine($"{sayResponse.UserName} {sayResponse.Message}");
                    break;
                case NickResponse nickResponse:
                    Console.WriteLine($"{nickResponse.OldUserName} is now {nickResponse.NewUserName}");
                    break;
            }
            return Actor.Done;
        });

        var client = context.Spawn(props);
        context.Send(server, new Connect
        {
            Sender = client
        });
        var nick = "Alex";
        while (true)
        {
            var text = Console.ReadLine();
            if (text.Equals("/exit"))
            {
                return;
            }
            if (text.StartsWith("/nick "))
            {
                var t = text.Split(' ')[1];
                context.Send(server, new NickRequest
                {
                    OldUserName = nick,
                    NewUserName = t
                });
                nick = t;
            }
            else
            {
                context.Send(server, new SayRequest
                {
                    UserName = nick,
                    Message = text
                });
            }
        }
    }
}
```

In turn, the client differs from the server in that in addition to creating an instance of the `Remote()`class, 

```c#
serialization.RegisterFileDescriptor(ChatReflection.Descriptor);
var remote = new Remote(system, serialization);
remote.Start("127.0.0.1", 0);
```

it needs to create `PID()` pointing to the remote server.

```c#
var server = new PID("127.0.0.1:8000", "chatserver");
```

After the client is initialized, commands are read from the console and then converted into messages and sent to the remote server.

```c#
while (true)
{
    var text = Console.ReadLine();
    if (text.Equals("/exit"))
    {
        return;
    }
    if (text.StartsWith("/nick "))
    {
        var t = text.Split(' ')[1];
        context.Send(server, new NickRequest
        {
            OldUserName = nick,
            NewUserName = t
        });
        nick = t;
     }
     else
     {
         context.Send(server, new SayRequest
         {
             UserName = nick,
             Message = text
             });
         }
}
```

To get acquainted with the full version of this example, you can see in the source code examples directory of Proto.Actor platform.