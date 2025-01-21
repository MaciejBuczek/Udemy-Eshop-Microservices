namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IAppDbContext dbContext) : ICommandHandler<DeleteOrderCommand, DeleteOrderCommandResult>
    {
        public async Task<DeleteOrderCommandResult> Handle(DeleteOrderCommand command, CancellationToken cancellationToken)
        {
            var orderId = OrderId.Of(command.OrderId);
            var order = await dbContext.Orders.FindAsync(orderId, cancellationToken) ??
                throw new OrderNotFoundException(command.OrderId.ToString());
        
            dbContext.Orders.Remove(order);
            await dbContext.SaveChangesAsync(cancellationToken);

            return new DeleteOrderCommandResult(true);
        }
    }
}