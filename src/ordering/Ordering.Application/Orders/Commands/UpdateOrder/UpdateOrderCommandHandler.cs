namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler(IAppDbContext dbContext) : ICommandHandler<UpdateOrderCommand, UpdateOrderResult>
    {
        public async Task<UpdateOrderResult> Handle(UpdateOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.Order.Id);
            var order = await dbContext.Orders.FindAsync(orderId, cancellationToken) ?? 
                throw new OrderNotFoundException(command.Order.Id.ToString());
            
            dbContext.Orders.Update(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new UpdateOrderResult(true);
        }

        public static void UpdateOrder(Order order, OrderDTO orderDTO)
        {
            var shippAddress = DomainModelParser.ParseAddress(orderDTO.ShippingAddress);
            var billingAddress = DomainModelParser.ParseAddress(orderDTO.BillingAddress);
            var payment = DomainModelParser.ParsePayment(orderDTO.Payment);

            order.Update(
                OrderName.Of(orderDTO.OrderName),
                shippAddress,
                billingAddress,
                payment,
                orderDTO.Status);
        }
    }
}
