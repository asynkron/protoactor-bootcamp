# Урок 2:  Хранимые акторы.

Хранимый актор действует в двух режимах: обрабатывает команды или восстанавливает последнее известное состояние из истории событии?. Команды - это сообщения, посылаемые актору для выполнения некоторой логики; события служат доказательством, что актор выполнил логику без ошибок. Первое, что мы сделаем, - определим команды и события для актора-калькулятора. В листинге определяются команды, которые калькулятор может выполнять, и события, возникающие после проверки допустимости команд калькулятором.

```protobuf
syntax = "proto3";
package messages;
option csharp_namespace = "Messages";

message AddCommand {
	double value = 1;
}

message SubtractCommand {
	double value = 1;
}

message DivideCommand {
	double value = 1;
}

message MultiplyCommand {
	double value = 1;
}

message PrintResultCommand {}

message ClearCommand {}

message ResetEvent {}

message AddedEvent {
	double value = 1;
}

message SubtractedEvent {
	double value = 1;
}

message DividedEvent {
	double value = 1;
}

message MultipliedEvent {
	double value = 1;
}
```

Теперь, после того как мы определили команды и события нам нужно добавить класс `Persistence()` в наш актор. Данный класс будет отвечать за сохранение и воспроизведение событий.

```c#
public Calculator(IProvider provider)
{
    _persistence = Persistence.WithEventSourcing(
    provider,
    "demo-app-id",
    ApplyEvent);
}
```

Далее добавляем в метод ReceiveAsync возможность обработки команд.

```c#
public async Task ReceiveAsync(IContext context)
{
    switch (context.Message)
    {
        case Started msg:
            Console.WriteLine("MyPersistenceActor - Started");
            await _persistence.RecoverStateAsync();
            break;

        case AddCommand msg:
            await _persistence.PersistEventAsync(new AddedEvent { Value = msg.Value });
            _result += msg.Value;
            break;

        case SubtractCommand msg:
            await _persistence.PersistEventAsync(new SubtractedEvent { Value = msg.Value });
            _result -= msg.Value;
            break;

        case DivideCommand msg:
            await _persistence.PersistEventAsync(new DividedEvent { Value = msg.Value });
            _result /= msg.Value;
            break;

        case MultiplyCommand msg:
            await _persistence.PersistEventAsync(new MultipliedEvent { Value = msg.Value });
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
```

Как вы видите реализация каждой команды просто выполняет нужное математическое действие и сохраняет результат в переменную _result. Отличие нашего актора от предыдущих акторов созданных в рамках данного учебного курса заключается в создание события, на каждое математическое действие, с дальнейшим их сохранением с помощью метода `persistence.PersistEventAsync` .

Для того что бы иметь возможность восстанавливать своё состояние наш актор должен вызывать метод `await _persistence.RecoverStateAsync();` в обработчике сообщения Started.

Для восстановления состояния актора с использованием событий. Актор должен содержать соответствующий метод для их обработки.

```c#
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
```

Таким образом. После того как нужное нам событие будет извлечено из базы данных оно будет передано методу ApplyEvent для дальнейшей обработки, с целью воссоздания внутреннего состояния нашего актора.

После того как, мы рассмотрели основные ключевые моменты. Давайте посмотрим как мы можем создать систему акторов с использованием Persistence и подходящего дата провайдера. Как мы уже говорили ранее, все произошедшие события должны быть сохранены. Для этой цели платформа Proto.Actor предоставляет несколько встроенных дата провайдеров для различных баз данных. А также интерфейс для реализации вашего собственного дата провайдера. 

Давайте посмотрим как мы можем использовать встроенный в Proto.Actor провайдер для базы данных SQL LIte в нашей системе акторов с поддержкой сохранения состояния.

```c#
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
```

Как вы видите из приведённого кода единственное отличие от предыдущих примеров из этого курса заключается в создании провайдера `SqliteProvider()` его настройки и передаче его актору с поддержкой сохранения.

```c#
var provider = new SqliteProvider(new SqliteConnectionStringBuilder { DataSource = "states.db" });
var props = Props.FromProducer(() => new Calculator(provider));
```

И так подведём итоги. Каждая команда в нашем акторе-калькуляторе превращается в событие и записывается в журнал, после чего в переменную `_result` записывается новый результат вычислений.

