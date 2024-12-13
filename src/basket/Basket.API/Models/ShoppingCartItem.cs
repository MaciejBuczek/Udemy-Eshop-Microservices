namespace Basket.API.Models
{
    public class ShoppingCartItem
    {
        public string Name { get; set; } = default!;
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Color { get; set; } = default!;
        public decimal Price { get; set; }
    }
}