# Lesson 11: Installing Proto.Actor.

To install Proto.Actor, we will use NuGet. NuGet contains all the necessary packages and dependencies to install Proto.Actor. The main package that we need to work with Photo.Actor is Proto. Actor =).

NuGet also contains several additional packages that we can install to access other features.

#### Proto.Schedulers.SimpleScheduler

The first one is the message scheduler package. Using this package, you can schedule a message to sent to the specified recipient with the specified delay and frequency.

#### Proto.Router

With this package, you will be able to organize the routing of messages in your application. Message routing used when you need to scale the application.

#### Proto.Persistence.*

This group of packages allows you to save the state of the actor using various technologies. For example, Proto.Persistence.SQLite allows you to save the internal state of the actor in the SQLite database.

#### Proto.Remote

The Proto.Remote package allows you to organize interaction between actors running on different servers.

#### Proto.Cluster.*

Proto.Cluster.* used for creating a cluster.

#### Proto.OpenTracing

This package allows you to debug actors using the [Open Tracking library](http://openracing.io/)



