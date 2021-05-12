using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MovieStreaming.Messages;
using Proto;

namespace MovieStreaming.Actors
{
    public class UserCoordinatorActor : IActor
    {
        private readonly Dictionary<int, PID> _users = new Dictionary<int, PID>();

        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case PlayMovieMessage msg:
                    ProcessPlayMovieMessage(context, msg);
                    break;

                case StopMovieMessage msg:
                    ProcessStopMovieMessage(context, msg);
                    break;
            }
            return Actor.Done;
        }

        private void ProcessPlayMovieMessage(IContext context, PlayMovieMessage msg)
        {
            CreateChildUserIfNotExists(context, msg.UserId);
            var childActorRef = _users[msg.UserId];
            context.Send(childActorRef, msg);
        }

        private void CreateChildUserIfNotExists(IContext context, int userId)
        {
            if (!_users.ContainsKey(userId))
            {
                var props = Props.FromProducer(() => new UserActor(userId));
                var pid = context.SpawnNamed(props, $"User{userId}");
                _users.Add(userId, pid);
                ColorConsole.WriteLineCyan($"UserCoordinatorActor created new child UserActor for {userId} (Total Users: {_users.Count})");
            }
        }

        private void ProcessStopMovieMessage(IContext context, StopMovieMessage msg)
        {
            CreateChildUserIfNotExists(context, msg.UserId);
            var childActorRef = _users[msg.UserId];
            context.Send(childActorRef, msg);
        }
    }
}
