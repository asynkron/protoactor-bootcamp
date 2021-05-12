using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieStreaming.Messages;
using Proto;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : IActor
    {

        private string _currentlyWatching;

        public PlaybackActor() => Console.WriteLine("Creating a PlaybackActor");
        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case Started msg:
                    ProcessStartedMessage(msg);
                    break;

                case PlayMovieMessage msg:
                    ProcessPlayMovieMessage(msg);
                    break;

                case Recoverable msg:
                    ProcessRecoverableMessage(context, msg);
                    break;

                case Stopping msg:
                    ProcessStoppingMessage(msg);
                    break;
            }
            return Actor.Done;
        }

        private void ProcessStartedMessage(Started msg)
        {
            ColorConsole.WriteLineGreen("PlaybackActor Started");
        }

        private void ProcessPlayMovieMessage(PlayMovieMessage msg)
        {
            ColorConsole.WriteLineYellow($"PlayMovieMessage {msg.MovieTitle} for user {msg.UserId}");
        }

        private void ProcessRecoverableMessage(IContext context, Recoverable msg)
        {
            PID child;

            if (context.Children == null || context.Children.Count == 0)
            {
                var props = Props.FromProducer(() => new ChildActor());
                child = context.Spawn(props);
            }
            else
            {
                child = context.Children.First();
            }

            context.Forward(child);
        }

        private void ProcessStoppingMessage(Stopping msg)
        {
            ColorConsole.WriteLineGreen("PlaybackActor Stopping");
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;

            ColorConsole.WriteLineYellow($"User is currently watching '{_currentlyWatching}'");
        }

        private void SoptPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow($"User has stopped watching '{_currentlyWatching}'");
            _currentlyWatching = null;
        }
    }
}
