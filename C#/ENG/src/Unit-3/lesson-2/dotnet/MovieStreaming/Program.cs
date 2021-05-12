using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using MovieStreaming.Actors;
using MovieStreaming.Messages;
using Proto;

namespace MovieStreaming
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = new ActorSystem();
            Console.WriteLine("Actor system created");

            var props = Props.FromProducer(() => new PlaybackActor()).WithChildSupervisorStrategy(new OneForOneStrategy(Decider.Decide, 1, null));
            var pid = system.Root.Spawn(props);

            system.Root.Send(pid, new PlayMovieMessage("The Movie", 44));
            system.Root.Send(pid, new PlayMovieMessage("The Movie 2", 54));
            system.Root.Send(pid, new PlayMovieMessage("The Movie 3", 64));
            system.Root.Send(pid, new PlayMovieMessage("The Movie 4", 74));

            Thread.Sleep(50);
            Console.WriteLine("press any key to restart actor");
            Console.ReadLine();

            system.Root.Send(pid, new Recoverable());

            Console.WriteLine("press any key to stop actor");
            Console.ReadLine();
            system.Root.Stop(pid);

            Console.ReadLine();
        }

        private class Decider
        {
            public static SupervisorDirective Decide(PID pid, Exception reason)
            {
                return SupervisorDirective.Restart;
            }
        }
    }
}
