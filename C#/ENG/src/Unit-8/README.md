# Proto.Actor Bootcamp - Module 8: Proto.Actor Cluster.

<img src="images/protowhite.png" alt="protowhite" style="float: left; zoom: 20%;" />

## Concepts you'll learn.

In Chapter 7, you learned how to create distributed applications with a fixed number of nodes. The static membership approach is simple but does not have a ready-made load-balancing or fault-tolerance solution. A cluster allows you to dynamically increase and decrease the number of nodes used by a distributed application and eliminates the fear of having a single point of failure.

Many distributed applications operate in environments that are not completely under your control, such as cloud platforms or data centers, which are dispersed throughout the world. The larger the cluster, the higher the failure probability. However, you have the tools at your disposal to monitor and manage the cluster lifecycle. In the first section of this module, we will see how a node becomes a member of a cluster, how to receive events related to cluster membership, and how to identify disaster nodes in the cluster.

First, we will create a cluster application that calculates each word's occurrence in a certain fragment of text. During the development of the example, you will learn how routers can be used to interact with actors in a cluster, create stable and coordinated processes from multiple actors in a cluster, and test clustered actor systems.

## Table of Contents

1. [Why do you need clusters.](lesson-1/README.md)
2. [Membership in the cluster.](lesson-2/README.md)
3. [Joining to the cluster.](lesson-3/README.md)
4. [Processing tasks in the Cluster.](lesson-4/README.md)
5. [Running a Cluster.](lesson-5/README.md)
6. [How to distribute tasks by using routers.](lesson-6/README.md)
7. [Reliable task processing.](lesson-7/README.md)
8. [Cluster Testing.](lesson-8/README.md)
