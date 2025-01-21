namespace Ordering.Application.Orders.Commands.UpdateOrder
{
    public record UpdateOrderResult(bool IsSuccess);
    public record UpdateOrderCommand(OrderDTO Order) : ICommand<UpdateOrderResult>;

    public class UpdateOrderCommandValidator : AbstractValidator<UpdateOrderCommand>
    {
        public UpdateOrderCommandValidator()
        {
            RuleFor(x => x.Order.Id).NotEmpty();
            RuleFor(x => x.Order.OrderName).NotEmpty();
            RuleFor(x => x.Order.CustomerId).NotEmpty();
        }
    }
}
