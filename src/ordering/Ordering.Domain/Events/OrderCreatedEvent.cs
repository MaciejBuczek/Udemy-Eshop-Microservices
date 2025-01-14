namespace Ordering.Domain.Events
{
    internal record OrderCreatedEvent(Order Order) : IDomainEvent;
}
