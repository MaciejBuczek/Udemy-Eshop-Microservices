namespace Basket.API.Features.GetBasket
{
    public record GetBasketResult(ShoppingCart ShoppingCart);
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    internal class GetBasketQueryHandler(IBasketRepository repository) : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasket(request.UserName, cancellationToken);
            return new GetBasketResult(basket);
        }
    }
}
