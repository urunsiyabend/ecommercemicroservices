namespace Ordering.Domain.ValueObjects
{
    public record OrderItemID
    {
        public Guid Value { get; }
        private OrderItemID(Guid value) => Value = value;
        public static OrderItemID Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("OrderItemID cannot be empty", nameof(value));
            }
            return new OrderItemID(value);
        }
    }
}
