# Lesson 2: Overview of the application that demonstrates the supervisor's capabilities and the actors hierarchy.

So let's look at the hierarchy of actors that we will be creating throughout this module.

We will start creating our application with the actor that is at the top of the actor hierarchy. This actor will be called `PlaybackActor ()`. It will be responsible directly for playing movies. This actor will have two child actors. The first child actor called `UserCoordinatorActor()` is responsible for creating one or more `Useractors()`.

We create an instance of ' UserActor()` for each end-user who watches the movie. We created the initial version of `UserActor () ' in the previous module.

![](images/4_2_1.png)

`PlaybackActor()` also has a second child actor called `PlaybackStatisticsActor()` , this actor is the parent actor for the actor `MoviePlayCounterActor()`.  The role of the `MoviePlayCounterActor()` actor is to count the number of times each movie is played back.

![](images/4_2_2.png)

We also have a couple of messages `PlayMovieMessage()` and `StopMovieMessage()`. These messages are used to start and stop movie playback.

![](images/4_2_3.png)

These messages have a UserId, and when they fall into `UserCoordinatorActor ()` ` 'UserCoordinatorActor ()' will check if there is no suitable instance of `UserActor()` for this UserId, then `UserCoordinatorActor()` will create  apropriate `UserActor()`. And Next `UserCoordinatorActor () ' will send messages to the desired `UserActor()` to start or stop playback of the movie.

![](images/4_2_4.png)

When `UserActor()` starts playing the movie, it will create an instance of the message `IncrementPlayCountMessage()` and send it `MoviePlayCounterActor()` . In turn, `MoviePlayCounterActor()` will increase the movie playback counter by one. This way, `MoviePlayCounterActor()` will track how many times each movie has been played back.

