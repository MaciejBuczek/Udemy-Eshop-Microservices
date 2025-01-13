namespace Ordering.Domain.Events
{
    internal record OderUpdatedEvent(Order Order) : IDomainEvent;
}
