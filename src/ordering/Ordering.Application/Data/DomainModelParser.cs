namespace Ordering.Application.Data
{
    public static class DomainModelParser
    {
        public static Address ParseAddress(AddressDTO addressDTO)
        {
            return Address.Of(
                addressDTO.FirstName,
                addressDTO.LastName,
                addressDTO.EmailAddress,
                addressDTO.AddressLine,
                addressDTO.Country,
                addressDTO.State,
                addressDTO.ZipCode);
        }

        public static Payment ParsePayment(PaymentDTO paymentDTO)
        {
            return Payment.Of(
                paymentDTO.CardName,
                paymentDTO.CardNumber,
                paymentDTO.Expiration,
                paymentDTO.Cvv,
                paymentDTO.PaymentMethod);
        }
    }
}
