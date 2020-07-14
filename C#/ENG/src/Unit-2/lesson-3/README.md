# Lesson 3: Defining Messages.

After we created our actors, the next step is to define the messages that we will send between the actors. In addition to sending simple primitive data types that exist in .Net, such as string or int, we can also define our custom message classes. 

Message classes can be written in two ways, using C# language or platform-independent Protobuf language.

The difference between these ways of creating messages is that when you create a C# message, you can use all data types available on the .Net platform. But when using Protobuf, the choice of data types is very limited. But with Protobuf, you can create actors that can interact with actors written in other programming languages, which is not possible with messages written in C#. 

Since the main feature of the Proto.Actor platform is the ability to write actors in different programming languages. You should always try to use Protobuf for writing messages. And use C# only in extraordinary cases. To allow your system to interact with actors in other programming languages in the future.

#### How to create a message with using C#.

So, to start, we'll create a simple class in our IDE, unlike creating an actor, where we had to inherit from a special base class. The message class does not need to inherit from any base class or interface.

Next, we can add properties to our message class to define the data we want to pass inside the message. When the message sent to the actor, the actor can extract this data from the properties and use it.

The last thing we should do is make the properties of our class read-only. And set a constructor that takes the initial values of properties and then sets them using closed setters. This way, we can be sure that once we have created an instance of a message, it will be read-only.

At the end, our message should look like this.

```c#
private class Hello
{
    public string Who { get; }

    public Hello(string who)
    {
        Who = who;
    }
}
```

When creating messages, we should try to adhere to certain best practices that exist when working with messages.

The main rule is that we should not pass a variable state between actors. One of the main advantages of using the actor model is that it dramatically simplifies parallel programming. If we start passing variable states between actors, we will return to the old problems related to blocking shared resources. 

Therefore, we must create immutable messages. In other words, our messages must be thread-safe. It will allow us to transmit them anywhere in the system without having to worry about ssharing the variable states between actors. 

#### How to create a message using gRPC and Protobuf.

**gRPC** is a high-performance framework developed by Google for Remote Procedure Call (RPC) that running on top of HTTP/2.

gRPC is easy to use, great for creating distributed systems (microservices) and API. It has built-in support for load balancing, tracing, authentication, and service viability checks. It is possible to create client libraries for working with the backend in 10 languages—high performance achieved through the use of HTTP/2 and Protocol Buffers.

## Protocol Buffers (protobuf)

[Protobuf](https://developers.google.com/protocol-buffers/) is the default format for transmitting data between client and server. Using strict field typing and binary format for transmitting structured data consumes less resources. The execution time of the serialization/decerialization process is much shorter, as well as the message size, unlike JSON/XML.

The Interface Definition Language (IDL) is used for writing protobuf files. For example, to describe the message data structure, you need to add message, the structure name, and inside the field type, name, and number. Field numbers are very important for backward compatibility, so you should not change their sequence when adding or removing fields. Old numbers can be reserved.

### Actor definition

gRPC uses the "contract-first" approach, i.e., a contract is defined first - a general service definition that defines an interaction mechanism. Let's consider a simple contract.

```protobuf
syntax = "proto3";
 
option csharp_namespace = "SimpleGrpcService";
 
package greet;

message HelloRequest {
  string name = 1;
}
 
message HelloReply {
  string message = 1;
}
```

The definition of this file may resemble the C# syntax, but in reality, it is the proto syntax used to describe the gPRC service. Although in general it is a C-like syntax, it is therefore not so difficult to navigate it.

The first line defines the type of syntax to be used:

```protobuf
syntax = "proto3";
```

In this case, the "proto3" syntax is used. At the moment, there is also a "proto 2" version, but it is older and practically not used.

Further, we define the namespace that will be used with this service:

```protobuf
option csharp_namespace = "TestGrpcService";
```

By default, this is the project name. And accordingly, the generated classes will be placed in the given namespace.

The next line uses the **package** operator to define the package name:

```protobuf
package greet;
```

In this example, the package is called "great". Installing the package allows you to resolve name conflicts when there are entities with the same names.

Next starts the definition of used the messages:

```protobuf
message HelloRequest {
  string name = 1;
}
 
message HelloReply {
  string message = 1;
}
```

The message is an entity that contains the data to be sent. The message defines a set of fields for which the type is defined. Each field represents some piece of information submitted in the message. So, in both messages, two fields of the string type are defined: each message will send a certain string.

Proto3 supports the use of many standard primitive types that are used in some of the most popular programming languages, such as `bool, int32, float, double' and `string'. Thus, it is possible to send data that are different in nature. You may also use earlier defined types of messages as the types of message fields.

Each field in the message is assigned a unique number. In the example above, the fields in both messages are assigned 1 (in `string name = 1 'or in' string message = 1'). These values allow you to identify values of fields in binary format when encoding and receiving messages. When using numbers from 1 to 15 as a value in the binary representation, an additional byte is added to the message. Values from 16 to 2047 add two additional bytes. Therefore, for the most frequently used data in the message, it is better to specify values from 1 to 15. Acceptable values: from 1 to 536870911 (with the exception of the number range from 19000 to 19999).

Next, the compiler will generate a class that corresponds to the definition in the file `*.proto`.

A detailed guide to protobuf syntax can be found at https://developers.google.com/protocol-buffers/docs/proto3 и https://developers.google.com/protocol-buffers/docs/csharptutorial

Using this approach, we can abstract from a specific language and describe the server-client interaction in terms of the Proto. And then for each specific language (C#, Java, etc.) we can define the corresponding implementation.