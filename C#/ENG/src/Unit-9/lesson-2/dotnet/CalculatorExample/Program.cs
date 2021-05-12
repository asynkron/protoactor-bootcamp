using System;
using Messages;
using Microsoft.Data.Sqlite;
using Proto;
using Proto.Persistence.Sqlite;

namespace CalculatorExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var system = new ActorSystem();
            var context = new RootContext(system);
            var provider = new SqliteProvider(new SqliteConnectionStringBuilder { DataSource = "states.db" });

            var props = Props.FromProducer(() => new Calculator(provider));
            var pid = context.Spawn(props);

            system.Root.Send(pid, new AddCommand {Value = 100});
            system.Root.Send(pid, new SubtractCommand { Value = 50 });

            system.Root.Send(pid, new PrintResultCommand ());

            system.Root.Poison(pid);

            pid = context.Spawn(props);

            system.Root.Send(pid, new PrintResultCommand());

            Console.ReadLine();
        }
    }
}
