# Lesson 3: Use of Proto.Actor in different types of applications.

So, what types of applications we can use together with Proto.Actor?  We can use Proto.Actor both on the server and client sides.

### Server side

#### Services backend (WEB API)

By using Proto.Actor on the server-side, we can build services based on REST or SOAP API. This way, you can redirect requests coming from WEB API clients to your actor model.

#### Web App

You can create a web application that will be using ASP.NET or Angular to interact with the user. And transfer his requests to our app.

#### Windows Service

You can use Proto.Actor as Windows Service.

### Client side

#### Console Application

You can also use Proto.Actor on the client-side as a console application.  After the user enters the command in the console, this command will be translated to message and sending to our actor model.

#### WPF Application 

In this type of app, we redirect user requests from the GUI to the actor system. This will allow us to create a reactive, parallel application. Where the task execution is entirely separate from the GUI.