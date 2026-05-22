namespace SharedKernel;

// Base type for events published across bounded contexts.

public abstract record IntegrationEvent
{
    // Unique identifier for the event instance; enables idempotent consumers and deduplication.
    public Guid EventId { get; init; } = Guid.NewGuid();

    // Wall-clock time the event was created. Consumers should treat this as informational,
    // not as a logical ordering key (use Version or a stream offset for ordering).
    public DateTimeOffset OccuredAt { get; init; } = DateTimeOffset.Now;

    // Schema version for the event payload. Bump this when the event's shape changes
    // so downstream consumers can route to the appropriate handler.
    public int Version { get; init; } = 1;
}

// Abstraction over the underlying transport (e.g. message broker) used to publish
// integration events. Implementations live in infrastructure projects.
public interface IIntegrationEventBus
{
    // Publishes an event to the bus. The task completes once the event has been
    // accepted by the transport, not necessarily once consumers have processed it.
    Task PublishAsync(IntegrationEvent @event, CancellationToken ct = default);
}

