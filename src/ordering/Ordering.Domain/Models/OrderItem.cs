namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItemId>
    {
        internal OrderItem(OrderId orderId, ProductId productId, int quantity, decimal price) 
        {
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        protected OrderItem() { }

        public OrderId OrderId { get; private set; } = default!;
        public ProductId ProductId { get; private set; } = default!;
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }
    }
}