using Discount.GRPC;

namespace Basket.API.Features.StoreBasket
{
    public record StoreBasketResult(string UserName);
    public record StoreBasketCommand(ShoppingCart ShoppingCart) : ICommand<StoreBasketResult>;

    internal class StoreBasketCommandHandler(IBasketRepository repository,
        DiscountProtoService.DiscountProtoServiceClient discountProto)
        : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            foreach (var item in command.ShoppingCart.Items)
            {
                var coupon = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.Name }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;
            }

            await repository.StoreBasket(command.ShoppingCart, cancellationToken);

            return new StoreBasketResult(command.ShoppingCart.UserName);
        }
    }
}