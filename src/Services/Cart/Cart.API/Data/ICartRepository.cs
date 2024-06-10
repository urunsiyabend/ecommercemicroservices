namespace Cart.API.Data
{
    public interface ICartRepository
    {
        Task<ShoppingCart> GetCartAsync(string username, CancellationToken cancellationToken = default);
        Task<ShoppingCart> StoreCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default);
        Task<bool> DeleteCartAsync(string username, CancellationToken cancellationToken = default);
    }
}
