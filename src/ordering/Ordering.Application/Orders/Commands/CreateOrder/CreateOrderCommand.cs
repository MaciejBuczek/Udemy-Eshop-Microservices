namespace Ordering.Application.Orders.Commands.CreateOrder
{
    public record CreateOrderResult(Guid Id);
    public record CreateOrderCommand(OrderDTO Order) : ICommand<CreateOrderResult>;

    public class CreateOrderCommandValidetor : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidetor()
        {
            RuleFor(x => x.Order.OrderName).NotEmpty();
            RuleFor(x => x.Order.CustomerId).NotEqual(Guid.Empty);
            RuleFor(x => x.Order.OrderItems).NotEmpty();
        }
    }
}