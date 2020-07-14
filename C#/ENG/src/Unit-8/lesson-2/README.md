# Lesson 2:  Membership in the cluster.

Let's start by creating a cluster. A cluster consists of a master and slave nodes. The figure shows the cluster that we are about to create.

![](images/8_2_1.png)

The master nodes control the execution of word counting tasks. The slave nodes request tasks from the master, process their text parts and return the results to the master. The master node generates the final result as soon as all parts of the task are completed. The task is restarted if a master or a slave node fails.

In the picture, you can also see another type of node that is needed in the cluster, namely the seed nodes. The seed nodes are needed to start the cluster. In the next section, we will see how the nodes become seed nodes and how they can join and detach from the cluster. We will take a closer look at creating a cluster and we will experiment with forming and modifying a simple cluster in the console. You will learn about the different states that member nodes go through and how to subscribe to change notifications.

