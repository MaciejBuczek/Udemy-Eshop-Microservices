namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IAppDbContext dbContext) : ICommandHandler<CreateOrderCommand, CreateOrderResult>
    {
        public async Task<CreateOrderResult> Handle(CreateOrderCommand command, CancellationToken cancellationToken)
        {
            var order = CreateOrder(command.Order);
            
            dbContext.Orders.Add(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new CreateOrderResult(order.Id.Id);
        }

        private static Order CreateOrder(OrderDTO orderDTO)
        {

            var shippingAddress = DomainModelParser.ParseAddress(orderDTO.ShippingAddress);
            var billingAddress = DomainModelParser.ParseAddress(orderDTO.BillingAddress);

            var payment = DomainModelParser.ParsePayment(orderDTO.Payment);

            var order = Order.Create(
                OrderId.Of(Guid.NewGuid()),
                CustomerId.Of(orderDTO.CustomerId),
                OrderName.Of(orderDTO.OrderName),
                shippingAddress,
                billingAddress,
                payment);

            foreach(var orderItemDTO in orderDTO.OrderItems)
            {
                order.Add(ProductId.Of(orderItemDTO.ProductId), orderItemDTO.Quantity, orderItemDTO.Price);
            }

            return order;
        }
    }
}