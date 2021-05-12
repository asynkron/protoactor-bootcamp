using System;
using System.Collections.Generic;
using System.Text;
using Proto;

namespace MovieStreaming.Messages
{
    public class ResponseActorPidMessage
    {
        public string ActorName { get; }

        public PID Pid { get; }

        public ResponseActorPidMessage(string actorName, PID pid)
        {
            ActorName = actorName;
            Pid = pid;
        }
    }
}
