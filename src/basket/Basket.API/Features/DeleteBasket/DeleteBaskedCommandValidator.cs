namespace Basket.API.Features.DeleteBasket
{
    public class DeleteBaskedCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBaskedCommandValidator()
        {
            RuleFor(x => x.UserName).NotEmpty();
        }
    }
}