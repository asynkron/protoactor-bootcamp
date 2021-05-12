using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Proto;

namespace RouterExample.Actors
{
    public class CleanupActor : IActor
    {
        public Task ReceiveAsync(IContext context)
        {
            var msg = context.Message;
            if (msg is string r)
            {
                Console.WriteLine($"Cleanup Actor get a message: {r}");
            }
            return Actor.Done;
        }
    }
}
