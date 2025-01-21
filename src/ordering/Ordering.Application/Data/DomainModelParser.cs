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

        public static OrderDTO ParseOrderDTO(Order order)
        {
            return new OrderDTO(
                order.Id.Id,
                order.CustomerId.Id,
                order.OrderName.Name,
                ParseAddressDTO(order.ShippingAddress),
                ParseAddressDTO(order.BillingAddress),
                ParsePaymentDTO(order.Payment),
                order.Status,
                order.OrderItems.Select(oi => ParseOrderItemDTO(oi)).ToList());
        }

        public static AddressDTO ParseAddressDTO(Address address)
        {
            return new AddressDTO(
                address.FirstName,
                address.LastName,
                address.EmailAddress,
                address.AddressLine,
                address.Country,
                address.State,
                address.ZipCode);
        }

        public static PaymentDTO ParsePaymentDTO(Payment payment)
        {
            return new PaymentDTO(
                payment.CardName,
                payment.CardNumber,
                payment.Expiration,
                payment.CVV,
                payment.PaymentMethod);
        }

        public static OrderItemDTO ParseOrderItemDTO(OrderItem orderItem)
        {
            return new OrderItemDTO(
                orderItem.OrderId.Id,
                orderItem.ProductId.Id,
                orderItem.Quantity,
                orderItem.Price);
        }
    }
}
