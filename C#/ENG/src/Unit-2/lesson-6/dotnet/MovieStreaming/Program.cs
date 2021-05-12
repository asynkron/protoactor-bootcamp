using System;
using System.Threading.Tasks;
using Proto;

namespace MovieStreaming
{
    public class PlaybackActor : IActor
    {
        public PlaybackActor() => Console.WriteLine("Creating a PlaybackActor");
        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case string movieTitle:
                    Console.WriteLine($"Received movie title {movieTitle}");
                    break;
                case int userId:
                    Console.WriteLine($"Received user ID {userId}");
                    break;
            }
            return Actor.Done;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var system = new ActorSystem();
            Console.WriteLine("Actor system created");

            var props = Props.FromProducer(() => new PlaybackActor());
            var pid = system.Root.Spawn(props);

            system.Root.Send(pid, "The Movie");
            system.Root.Send(pid, 44);

            Console.ReadLine();
        }
    }
}
