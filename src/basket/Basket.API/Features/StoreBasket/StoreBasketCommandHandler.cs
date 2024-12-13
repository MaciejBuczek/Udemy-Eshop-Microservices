namespace Basket.API.Features.StoreBasket
{
    public record StoreBasketResult(string UserName);
    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

    internal class StoreBasketCommandHandler(IBasketRepository repository) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await repository.StoreBasket(command.ShoppingCart, cancellationToken);
            return new StoreBasketResult(command.ShoppingCart.UserName);
        }
    }
}