namespace Ordering.Domain.ValueObjects
{
    public record OrderName
    {

        private const int _DefaultMaxLength = 5;

        public string Name { get; private set; }

        private OrderName(string name) => Name = name;

        public static OrderName Of(string name)
        {
            ArgumentException.ThrowIfNullOrEmpty(name);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(name.Length, _DefaultMaxLength);

            return new OrderName(name);
        }
    }
}
