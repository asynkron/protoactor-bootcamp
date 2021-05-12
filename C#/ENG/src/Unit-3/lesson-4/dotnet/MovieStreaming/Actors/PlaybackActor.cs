using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieStreaming.Messages;
using Proto;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : IActor
    {
        public PlaybackActor() => Console.WriteLine("Creating a PlaybackActor");
        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case PlayMovieMessage msg:
                    Console.WriteLine($"Received movie title {msg.MovieTitle}");
                    Console.WriteLine($"Received user ID {msg.UserId}");
                    break;

                case Stopped msg:
                    Console.WriteLine("actor is Stopped");
                    break;
            }
            return Actor.Done;
        }
    }
}
