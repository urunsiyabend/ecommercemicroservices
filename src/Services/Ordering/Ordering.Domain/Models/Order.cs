namespace Ordering.Domain.Models
{
    public class Order : Aggregate<OrderID>
    {
        private readonly List<OrderItem> _orderItems = new();
        public IReadOnlyList<OrderItem> OrderItems => _orderItems.AsReadOnly();
        public CustomerID CustomerId { get; private set; }
        public OrderName OrderName { get; private set; } = default!;
        public Address ShippingAddress { get; private set; } = default!;
        public Address BillingAddress { get; private set; } = default!;
        public Payment Payment { get; private set; } = default!;
        public OrderStatus OrderStatus { get; private set; } = OrderStatus.Pending;

        public decimal TotalPrice
        {
            get => _orderItems.Sum(item => item.Price * item.Quantity);
            private set { }
        }

        public static Order Create(OrderID orderID, CustomerID customerId, OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
        {
            var order = new Order
            {
                Id = orderID,
                CustomerId = customerId,
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment
            };

            order.AddDomainEvent(new OrderCreatedEvent(order));

            return order;
        }

        public static Order Update(OrderName orderName, Address shippingAddress, Address billingAddress, Payment payment)
        {
            var order = new Order
            {
                OrderName = orderName,
                ShippingAddress = shippingAddress,
                BillingAddress = billingAddress,
                Payment = payment
            };

            order.AddDomainEvent(new OrderUpdatedEvent(order));

            return order;
        }

        public void AddOrderItem(ProductID productId, int quantity, decimal price)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(quantity, nameof(quantity));
            ArgumentOutOfRangeException.ThrowIfNegative(price, nameof(price));

            var orderItem = new OrderItem(Id, productId, quantity, price);
            _orderItems.Add(orderItem);
        }

        public void RemoveOrderItem(OrderItemID orderItemId)
        {
            var orderItem = _orderItems.FirstOrDefault(item => item.Id == orderItemId);
            if (orderItem is null)
            {
                throw new InvalidOperationException("Order item not found.");
            }

            _orderItems.Remove(orderItem);
        }
    }
}
