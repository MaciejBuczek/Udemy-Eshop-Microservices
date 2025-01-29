namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) 
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration Event handlerd: {IntegrationEvent}", context.Message.GetType().Name);
            var command = MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }

        private static CreateOrderCommand MapToCreateOrderCommand(BasketCheckoutEvent checkoutEvent)
        {
            var addressDTO = new AddressDTO(checkoutEvent.FirstName, checkoutEvent.LastName, checkoutEvent.EmailAddress, checkoutEvent.AddressLine, checkoutEvent.Country, checkoutEvent.State, checkoutEvent.ZipCode);
            var paymentDTO = new PaymentDTO(checkoutEvent.CardName, checkoutEvent.CardNumber, checkoutEvent.Expiration, checkoutEvent.CVV, checkoutEvent.PaymentMethod);
            var orderId = Guid.NewGuid();

            var orderDTO = new OrderDTO(
                Id: orderId,
                CustomerId: checkoutEvent.CustomerId,
                OrderName: checkoutEvent.Username,
                ShippingAddress: addressDTO,
                BillingAddress: addressDTO,
                Payment: paymentDTO,
                Status: OrderStatus.Pending,
                OrderItems:
                [
                    new OrderItemDTO(orderId, new Guid("5334c996-8457-4cf0-815c-ed2b77c4ff61"),2, 500),
                    new OrderItemDTO(orderId, new Guid("c67d6323-e8b1-4bdf-9a75-b0d0d2e7e914"), 1, 400)
                ]);

            return new CreateOrderCommand(orderDTO);
        }
    }
}
