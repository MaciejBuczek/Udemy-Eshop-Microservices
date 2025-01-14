namespace Ordering.Domain.ValueObjects
{
    public record class Payment
    {
        private const int _cvvMaxLength = 3;

        public string CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; }

        protected Payment() { }

        private Payment(string cardName, string cardNumber, string expiration, string cVV, int paymentMethod)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = expiration;
            CVV = cVV;
            PaymentMethod = paymentMethod;
        }

        public static Payment Of(string cardName, string cardNumber, string expiration, string cvv, int paymentMethod)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(expiration);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, _cvvMaxLength);

            return new Payment(cardName, cardNumber, expiration, cvv, paymentMethod);
        }
    }
}