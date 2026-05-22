namespace Ordering.Infrastructure;

using SharedKernel;
using System.Collections.Concurrent;

public class InMemoryIntegrationEventBus : IIntegrationEventBus
{
    private readonly ConcurrentDictionary<Type, List<Func<IntegrationEvent, CancellationToken, Task>>> _handlers = new();
    public void Subscribe<TEvent>(Func<TEvent, CancellationToken, Task> handler)
         where TEvent : IntegrationEvent
    {
        var list = _handlers.GetOrAdd(typeof(TEvent), _ => new());
        list.Add((e, ct) => handler((TEvent)e, ct));
    }

    public async Task PublishAsync(IntegrationEvent @event, CancellationToken ct = default)
    {
        if (_handlers.TryGetValue(@event.GetType(), out var handlers))
        {
            foreach (var handler in handlers)
                await handler(@event, ct);
        }
    }
}
