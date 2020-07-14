# Proto.Actor Bootcamp - Module 5: Message Routing.

<img src="images/protowhite.png" alt="protowhite" style="float: left; zoom: 20%;" />

## Concepts you'll learn.

Message routers are necessary when you need to organize scalability management. For example, to handle the increased load, you need to create multiple instances of the same actor, and the router will decide to which instance to send the next message. We will begin this module by describing the "Router" template and learn about the three reasons for using routing to control message stream:

- performance;
- message content; 
- state.

Then we will show you how to organize routing in each of the three cases. If performance or scalability is the main reason for implementing routing, then it is better to use routers supplied with the Proto.Actor platform, because they are highly optimized and will allow you to achieve maximum performance. But if routing is supposed to be used for sorting messages by their content, this is a sure sign that it is better to use regular actors for this task.

## Table of Contents

1. [Router pattern.](lesson-1/README.md)
2. [Load balancing with Proto.Actor routers.](lesson-2/README.md)
3. [Pool Router.](lesson-3/README.md)
4. [Group Router.](lesson-4/README.md)
5. [ConsistentHashing Router.](lesson-5/README.md)
6. [Implementation of the router pattern with using actors.](lesson-6/README.md)

