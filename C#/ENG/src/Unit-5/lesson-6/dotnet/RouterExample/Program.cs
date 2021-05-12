using System;
using Proto;
using RouterExample.Actors;
using RouterExample.Messages;

namespace RouterExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = new ActorSystem();

            var cleanupProps = Props.FromProducer(() => new CleanupActor());
            var cleanup = system.Root.Spawn(cleanupProps);

            var normalFlowProps = Props.FromProducer(() => new NormalFlowActor());
            var normalFlow = system.Root.Spawn(normalFlowProps);

            var switchRouterProps = Props.FromProducer(() => new SwitchRouter(normalFlow, cleanup));
            var switchRouter = system.Root.Spawn(switchRouterProps);

            var command = Console.ReadLine();

            {
                if (command != null)
                {
                    if (command.StartsWith("On"))
                    {
                        system.Root.Send(switchRouter, new RouteStateOn());
                    }
                    else if (command.StartsWith("Off"))
                    {
                        system.Root.Send(switchRouter, new RouteStateOff());
                    }
                    else
                    {
                        system.Root.Send(switchRouter, command);
                    }
                }
            }
            while (true);
        }
    }
}
