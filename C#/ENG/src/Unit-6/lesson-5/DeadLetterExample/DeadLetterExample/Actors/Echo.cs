using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace DeadLetterExample.Actors
{
    public class Echo : IActor
    {
        public Task ReceiveAsync(IContext context)
        {
            context.Respond(context.Message);
            return Actor.Done;
        }
    }
}
