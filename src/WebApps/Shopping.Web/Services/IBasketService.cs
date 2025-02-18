namespace Shopping.Web.Services
{
    public interface IBasketService
    {
        [Get("/basket-service/basket/{userName}")]
        Task<GetBasketResponse> GetBasket(string userName);

        [Post("/basket-service/basket")]
        Task<StoreBasketResponse> StoreBasket(StoreBasketRequest request);

        [Delete("/basket-service/basket/{userName}")]
        Task<DeleteBasketResponse> DeleteBasket(string userName);

        [Post("/basket-service/basket/checkout")]
        Task<CheckoutBasketResponse> CheckoutBasket(CheckoutBasketRequest request);

        public async Task<ShoppingCartModel> LoadUserBasket()
        {
            var username = "swn";
            ShoppingCartModel shoppingCartModel;
            try
            {
                var response = await GetBasket(username);
                shoppingCartModel = response.ShoppingCart;
            }
            catch (Exception)
            {
                shoppingCartModel = new ShoppingCartModel
                {
                    UserName = username,
                    Items = []
                };
            }

            return shoppingCartModel;
        }
    }
}
