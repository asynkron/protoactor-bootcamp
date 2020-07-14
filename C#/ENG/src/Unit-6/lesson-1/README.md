# Lesson 1: Channels Types

Let's begin our acquaintance with this module by describing the channel types used in the Proto.Actor platform. The first is channels **"point-to-point "** The name precisely defines characteristics of channels of this type: they connect one point (sender) to another (recipient). Often this is enough, but sometimes it is necessary to send a message to not many recipients. 

In this case, you will have to create several channels or use channels of the second type, **"publisher/subscriber "**. The main advantage of this type of channel is the ability to change recipients' composition during the application operation dynamically. Channels of this type in the Proto.Actor platform implement class `EventStream()`.

