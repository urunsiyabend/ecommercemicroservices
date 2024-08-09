namespace Ordering.Domain.Models
{
    public class Product : Entity<ProductID>
    {
        public string Name { get; private set; } = default!;
        public decimal Price { get; private set; } = default!;
        public static Product Create(string name, decimal price)
        {
            ArgumentException.ThrowIfNullOrEmpty(name, nameof(name));
            ArgumentOutOfRangeException.ThrowIfNegative(price, nameof(price));

            var product = new Product
            {
                Id = ProductID.Of(Guid.NewGuid()),
                Name = name,
                Price = price
            };

            return product;
        }
    }
}
