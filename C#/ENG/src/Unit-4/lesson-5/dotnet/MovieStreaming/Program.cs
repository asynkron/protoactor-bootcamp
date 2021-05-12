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
        static async Task Main(string[] args)
        {
            var system = new ActorSystem();
            Console.WriteLine("Actor system created");

            var props = Props.FromProducer(() => new PlaybackActor());
            var playbackPid = system.Root.Spawn(props);

            var actorPidMessage = await system.Root.RequestAsync<ResponseActorPidMessage>(playbackPid, new RequestActorPidMessage());
            var userCoordinatorActorPid = actorPidMessage.Pid;

            do
            {
                ShortPause();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ColorConsole.WriteLineGray("enter a command and hit enter");

                var command = Console.ReadLine();

                if (command != null)
                {
                    if (command.StartsWith("play"))
                    {
                        var userId = int.Parse(command.Split(',')[1]);
                        var movieTitle = command.Split(',')[2];

                        system.Root.Send(userCoordinatorActorPid, new PlayMovieMessage(movieTitle, userId));
                    }
                    else if (command.StartsWith("stop"))
                    {
                        var userId = int.Parse(command.Split(',')[1]);

                        system.Root.Send(userCoordinatorActorPid, new StopMovieMessage(userId));
                    }
                    else if (command == "exit")
                    {
                        Terminate();
                    }
                }
            } while (true);

            static void ShortPause()
            {
                Thread.Sleep(250);
            }

            static void Terminate()
            {
                Console.WriteLine("Actor system shutdown");
                Console.ReadKey();
                Environment.Exit(1);
            }
        }
    }
}
