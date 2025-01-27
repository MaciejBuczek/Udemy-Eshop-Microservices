namespace Basket.API.Features.CheckoutBasket
{
    public record CheckoutBasketResult(bool IsSuccess);
    public record CheckoutBasketCommand(BasketCheckoutDTO BasketCheckout) : ICommand<CheckoutBasketResult>;
    internal class CheckoutBasketHandler(IBasketRepository repository, IPublishEndpoint publishEndpoint)
        : ICommandHandler<CheckoutBasketCommand, CheckoutBasketResult>
    {
        public async Task<CheckoutBasketResult> Handle(CheckoutBasketCommand command, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(command.BasketCheckout.Username, cancellationToken);
            if (basket is null)
                return new CheckoutBasketResult(false);

            var eventMessage = command.BasketCheckout.Adapt<BasketCheckoutEvent>();
            eventMessage.TotalPrice = basket.TotalPrice;

            await publishEndpoint.Publish(eventMessage, cancellationToken);

            await repository.DeleteBasket(command.BasketCheckout.Username, cancellationToken);

            return new CheckoutBasketResult(true);
        }
    }

    public class CheckoutBasketCommandValidator : AbstractValidator<CheckoutBasketCommand>
    {
        public CheckoutBasketCommandValidator()
        {
            RuleFor(x => x.BasketCheckout).NotNull();
            RuleFor(x => x.BasketCheckout.Username).NotEmpty();
        }
    }
}
