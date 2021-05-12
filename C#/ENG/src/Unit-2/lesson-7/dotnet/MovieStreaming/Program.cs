using System;
using System.Threading.Tasks;
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

            var props = Props.FromProducer(() => new PlaybackActor());
            var pid = system.Root.Spawn(props);

            system.Root.Send(pid, new PlayMovieMessage("The Movie", 44));

            Console.ReadLine();
        }
    }
}
