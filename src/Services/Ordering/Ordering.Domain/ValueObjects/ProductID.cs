namespace Ordering.Domain.ValueObjects
{
    public record ProductID
    {
        public Guid Value { get; }
        private ProductID(Guid value) => Value = value;
        public static ProductID Of(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new ArgumentException("ProductID cannot be empty", nameof(value));
            }
            return new ProductID(value);
        }
    }
}
