using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieStreaming.Messages;
using Proto;

namespace MovieStreaming.Actors
{
    public class UserActor : IActor
    {
        private int _id;

        private string _currentlyWatching;

        private readonly Behavior _behavior;

        public UserActor(int id)
        {
            _id = id;
            Console.WriteLine("Creating a UserActor");
            _behavior = new Behavior(Stopped);
        }

        public Task ReceiveAsync(IContext context) => _behavior.ReceiveAsync(context);

        private Task Stopped(IContext context)
        {
            switch (context.Message)
            {
                case PlayMovieMessage msg:
                    _currentlyWatching = msg.MovieTitle;
                    ColorConsole.WriteLineYellow($"User is currently watching '{_currentlyWatching}'");
                    _behavior.Become(Playing);
                    break;
                case StopMovieMessage msg:
                    ColorConsole.WriteLineRed("Error: cannot stop if nothing is playing");
                    break;
                default:
                    ColorConsole.WriteLineCyan("UserActor has now become Stopped");
                    break;
            }

            return Actor.Done;
        }

        private Task Playing(IContext context)
        {
            switch (context.Message)
            {
                case PlayMovieMessage msg:
                    ColorConsole.WriteLineRed("Error: cannot start playing another movie before stopping existing one");
                    break;
                case StopMovieMessage msg:
                    ColorConsole.WriteLineYellow($"User has stopped watching '{_currentlyWatching}'");
                    _currentlyWatching = null;
                    _behavior.Become(Stopped);
                    break;
                default:
                    ColorConsole.WriteLineCyan("UserActor has now become Playing");
                    break;
            }

            return Actor.Done;
        }
    }
}
