namespace Ordering.Domain.Models
{
    public class Customer : Entity<CustomerID>
    {
        public string FirstName { get; private set; } = default!;
        public string LastName { get; private set; } = default!;
        public string Email { get; private set; } = default!;

        public static Customer Create(string firstName, string lastName, string email)
        {
            ArgumentException.ThrowIfNullOrEmpty(firstName, nameof(firstName));
            ArgumentException.ThrowIfNullOrEmpty(lastName, nameof(lastName));
            ArgumentException.ThrowIfNullOrEmpty(email, nameof(email));

            var customer = new Customer
            {
                Id = CustomerID.Of(Guid.NewGuid()),
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            return customer;
        }
    }
}
