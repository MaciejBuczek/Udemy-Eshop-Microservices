namespace Basket.API.Data
{
    internal class ChachedBaskedRepository(IBasketRepository repository, IDistributedCache cache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName, CancellationToken cancellationToken = default)
        {
            await cache.RemoveAsync(userName, cancellationToken);
            return await repository.DeleteBasket(userName, cancellationToken);
        }

        public async Task<ShoppingCart> GetBasket(string userName, CancellationToken cancellationToken = default)
        {
            var cached = await cache.GetStringAsync(userName, cancellationToken);
            if (string.IsNullOrEmpty(cached))
            {
                var basket = await repository.GetBasket(userName, cancellationToken);
                await cache.SetStringAsync(userName, JsonSerializer.Serialize(basket), cancellationToken);
                return basket;
            }

            return JsonSerializer.Deserialize<ShoppingCart>(cached) ??
                throw new ShoppingCartNotFoundException(userName);
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await cache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return await repository.StoreBasket(basket, cancellationToken);
        }
    }
}