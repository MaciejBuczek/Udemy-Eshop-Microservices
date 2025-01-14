namespace Ordering.Domain.ValueObjects
{
    public record OrderItemId
    {
        public Guid Id { get; private set; }

        private OrderItemId(Guid id) => Id = id;

        public static OrderItemId Of(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);
            if (id == Guid.Empty)
            {
                throw new DomainException("Order item id cannot be null");
            }

            return new OrderItemId(id);
        }
    }
}
