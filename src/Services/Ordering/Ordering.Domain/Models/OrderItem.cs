namespace Ordering.Domain.Models
{
    public class OrderItem : Entity<OrderItemID>
    {
        internal OrderItem(OrderID orderId, ProductID productId, int quantity, decimal price)
        {
            Id = OrderItemID.Of(Guid.NewGuid());
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public OrderID OrderId { get; private set; } = default!;
        public ProductID ProductId { get; private set; } = default!;
        public int Quantity { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
    }
}
