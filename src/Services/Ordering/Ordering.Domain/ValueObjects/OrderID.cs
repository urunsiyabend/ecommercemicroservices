namespace Ordering.Domain.ValueObjects
{
    public record OrderID
    {
        public Guid Value { get; }
        public OrderID(Guid value) => Value = value;
        public static OrderID Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("OrderID cannot be empty", nameof(value));
            }
            return new OrderID(value);
        }
    }
}
