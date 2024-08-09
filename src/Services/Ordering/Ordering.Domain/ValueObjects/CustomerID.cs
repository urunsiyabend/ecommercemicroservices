namespace Ordering.Domain.ValueObjects
{
    public record CustomerID
    {
        public Guid Value { get; }
        private CustomerID(Guid value) => Value = value;
        public static CustomerID Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("CustomerID cannot be empty", nameof(value));
            }
            return new CustomerID(value);
        }
    }
}
