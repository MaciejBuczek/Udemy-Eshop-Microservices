namespace Ordering.Domain.ValueObjects
{
    public record CustomerId
    {
        public Guid Id { get; private set; }

        private CustomerId(Guid id) => Id = id;

        public static CustomerId Of (Guid Id)
        {
            ArgumentNullException.ThrowIfNull(Id);

            if(Id == Guid.Empty)
            {
                throw new DomainException("CustomerId cannot be empty");
            }

            return new CustomerId(Id);
        }
    }
}
