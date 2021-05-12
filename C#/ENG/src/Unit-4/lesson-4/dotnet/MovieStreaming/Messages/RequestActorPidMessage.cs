using System;
using System.Collections.Generic;
using System.Text;

namespace MovieStreaming.Messages
{
    public class RequestActorPidMessage
    {
        public string ActorName { get; }

        public RequestActorPidMessage(string actorName)
        {
            ActorName = actorName;
        }
    }
}
