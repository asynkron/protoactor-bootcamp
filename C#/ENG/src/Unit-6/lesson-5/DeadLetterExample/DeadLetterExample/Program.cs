using System;
using System.Threading.Tasks;
using DeadLetterExample.Actors;
using DeadLetterExample.Messages;
using Proto;

namespace DeadLetterExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var system = new ActorSystem();
            var props = Props.FromProducer(() => new Echo());
            var pid = system.Root.Spawn(props);

            system.EventStream.Subscribe<DeadLetterEvent>(msg => Console.WriteLine($"Sender: {msg.Sender}, Pid: {msg.Pid}, Message: {msg.Message}"));

            system.Root.Send(pid, new TestMessage());
            await system.Root.PoisonAsync(pid);
            system.Root.Send(pid, new TestMessage());

            Console.ReadLine();
        }
    }
}
