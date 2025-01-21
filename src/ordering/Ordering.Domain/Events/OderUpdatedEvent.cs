namespace Ordering.Domain.Events
{
    public record OderUpdatedEvent(Order Order) : IDomainEvent;
}
