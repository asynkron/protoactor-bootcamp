using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Messages;
using Proto;
using Proto.Persistence;
using Proto.Persistence.SnapshotStrategies;

namespace CalculatorExample
{
    public class Calculator : IActor
    {
        private double _result = 0;
        private readonly Persistence _persistence;

        public Calculator(IProvider provider)
        {
            _persistence = Persistence.WithEventSourcing(
                provider,
                "demo-app-id",
                ApplyEvent);
        }

        private void ApplyEvent(Event @event)
        {
            switch (@event)
            {
                case RecoverEvent msg:
                    if (msg.Data is AddedEvent addedEvent)
                    {
                        _result += addedEvent.Value;
                    }
                    else if (msg.Data is SubtractedEvent subtractedEvent)
                    {
                        _result -= subtractedEvent.Value;
                    }
                    else if (msg.Data is DividedEvent dividedEvent)
                    {
                        _result /= dividedEvent.Value;
                    }
                    else if (msg.Data is MultipliedEvent multipliedEvent)
                    {
                        _result *= multipliedEvent.Value;
                    }
                    else if (msg.Data is ResetEvent resetEvent)
                    {
                        _result = 0;
                    }

                    break;
                case ReplayEvent msg:
                    break;
                case PersistedEvent msg:
                    break;
            }
        }

        public async Task ReceiveAsync(IContext context)
        {
            switch (context.Message)
            {
                case Started msg:
                    Console.WriteLine("MyPersistenceActor - Started");
                    await _persistence.RecoverStateAsync();
                    break;

                case AddCommand msg:
                    await _persistence.PersistEventAsync(new AddedEvent {Value = msg.Value});
                    _result += msg.Value;

                    break;

                case SubtractCommand msg:
                    await _persistence.PersistEventAsync(new SubtractedEvent {Value = msg.Value});
                    _result -= msg.Value;

                    break;

                case DivideCommand msg:
                    await _persistence.PersistEventAsync(new DividedEvent {Value = msg.Value});
                    _result /= msg.Value;

                    break;

                case MultiplyCommand msg:
                    await _persistence.PersistEventAsync(new MultipliedEvent {Value = msg.Value});
                    _result *= msg.Value;

                    break;

                case ClearCommand msg:
                    await _persistence.PersistEventAsync(new ResetEvent());
                    _result = 0;

                    break;

                case PrintResultCommand msg:
                    Console.WriteLine(_result);
                    break;
            }
        }
    }
}
