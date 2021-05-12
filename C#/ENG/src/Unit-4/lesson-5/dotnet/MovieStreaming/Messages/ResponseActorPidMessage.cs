using System;
using System.Collections.Generic;
using System.Text;
using Proto;

namespace MovieStreaming.Messages
{
    public class ResponseActorPidMessage
    {
        public PID Pid { get; }

        public ResponseActorPidMessage(PID pid)
        {
            Pid = pid;
        }
    }
}
