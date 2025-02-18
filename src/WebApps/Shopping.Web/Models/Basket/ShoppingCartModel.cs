namespace Shopping.Web.Models.Basket
{
    public class ShoppingCartModel
    {
        public string UserName { get; set; } = default!;
        public List<ShoppingCartItemModel> Items { get; set; } = []; 
        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public ShoppingCartModel(string userName)
        {
            UserName = userName;
        }
        public ShoppingCartModel()
        {

        }
    }
    public record GetBasketResponse(ShoppingCartModel ShoppingCart);
    public record StoreBasketRequest(ShoppingCartModel ShoppingCart);
    public record StoreBasketResponse(string UserName);
    public record DeleteBasketResponse(bool Success);
}
