namespace Ordering.Domain.ValueObjects
{
    public record ProductId
    {
        public Guid Id { get; private set; }

        private ProductId(Guid id) => Id = id;

        public static ProductId Of(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id);
            if (id == Guid.Empty)
            {
                throw new DomainException("Product id cannot be null");
            }

            return new ProductId(id);
        }
    }
}
