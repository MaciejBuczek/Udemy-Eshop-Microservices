namespace Ordering.Application.Orders.Commands.DeleteOrder
{
    public record DeleteOrderCommandResult(bool IsSuccess);
    public record DeleteOrderCommand(Guid OrderId) : ICommand<DeleteOrderCommandResult>;

    public class DeleteOrderCommandValidator : AbstractValidator<DeleteOrderCommand>
    {
        public DeleteOrderCommandValidator()
        {
            RuleFor(x => x.OrderId).NotEmpty();
        }
    }
}
