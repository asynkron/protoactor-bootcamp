# Урок 3: Пример работы с Proto.Actor Remote.

Для понимания принципов работы, с модулем **Proto.Remote** давайте создадим чат, который будет использовать **Proto.Remote** для передачи сообщений между участниками чата. Данное приложение будет состоять из трёх компонентов. Это сервер, клиент и библиотека с сообщения.

#### Библиотека с сообщения.

Мы начнём обсуждение нашего примера с рассмотрения библиотеки сообщений. Которыми будут обмениваться участники нашего чата. Все что нам нужно сделать для создания библиотеки сообщений, это создать новый проект и добавить в него следующий код.

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

Благодаря тому, что мы используем gRPC участниками нашего чата, могут стать клиенты, написанные на любом из поддерживаемых платформой Proto.Actor языков программирования.

#### Создание сервера.

Сервер представляет собой стандартное консольное приложение, которое ссылается на нашу сборку с сообщениями. 

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

Как вы видите, все что нам требуется, для того что бы начать получать сообщения от удаленной системы акторов, это создать экземпляр класса `Remote()` и запустить его вызовом метода `Start`

```c#
serialization.RegisterFileDescriptor(ChatReflection.Descriptor);
var remote = new Remote(system, serialization);
remote.Start("127.0.0.1", 8000);
```

После того как вы запустите экземпляр класса `Remote()`,  наш сервер откроет указанной порт и будет готов к приему входящих подключений. 

В остальном же, актор сервера ничем не отличается от обычных акторов.

#### Создание клиента.

Клиент также является консольным приложением и ссылается на библиотеку с сообщениями.

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

В свою очередь клиент отличается от сервера тем что кроме создания экземпляр класса `Remote()`, 

```c#
serialization.RegisterFileDescriptor(ChatReflection.Descriptor);
var remote = new Remote(system, serialization);
remote.Start("127.0.0.1", 0);
```

ему необходимо создать `PID()`  указывающий на удаленный сервер.

```c#
var server = new PID("127.0.0.1:8000", "chatserver");
```

После инициализации клиента происходит считывание команд с консоли с последуюзим преобразованием их в сообщения и отправки этих сообщений на удаленный сервер.

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

Ознакомиться с полной версией этого примера, вы сможете в каталоге examples исходного кода платформы Proto.Actor