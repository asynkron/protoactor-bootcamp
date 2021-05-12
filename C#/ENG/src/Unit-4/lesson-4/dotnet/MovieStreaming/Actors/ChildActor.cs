using System;
using System.Threading.Tasks;
using MovieStreaming.Messages;
using Proto;

namespace MovieStreaming.Actors
{
    public class ChildActor : IActor
    {
        public Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case Restarting msg:
                    ProcessRestartingMessage(msg);
                    break;

                case Recoverable msg:
                    ProcessRecoverableMessage(msg);
                    break;
            }
            return Actor.Done;
        }

        private void ProcessRecoverableMessage(Recoverable msg)
        {
            throw new Exception();
        }

        private void ProcessRestartingMessage(Restarting msg)
        {
            ColorConsole.WriteLineGreen("ChildActor Restarting");
        }
    }
}
