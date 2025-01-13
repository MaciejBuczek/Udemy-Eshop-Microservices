namespace Ordering.Domain.ValueObjects
{
    public record OrderId
    {
        public Guid Id { get; private set; }

        private OrderId(Guid id) => Id = id;

        public static OrderId Of(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);
            if(id == Guid.Empty)
            {
                throw new DomainException("Order Id cannot be null");
            }

            return new OrderId(id);
        }
    }
}
