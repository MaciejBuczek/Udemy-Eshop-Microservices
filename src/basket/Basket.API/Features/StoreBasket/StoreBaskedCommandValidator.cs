namespace Basket.API.Features.StoreBasket
{
    public class StoreBaskedCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBaskedCommandValidator()
        {
            RuleFor(x => x.ShoppingCart).NotNull();
            RuleFor(x => x.ShoppingCart.UserName).NotEmpty();
            RuleFor(x => x.ShoppingCart.Items).NotNull();
        }
    }
}
