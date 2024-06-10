namespace Cart.API.Data
{
    public class CartRepository(IDocumentSession session) : ICartRepository
    {
        public async Task<ShoppingCart> GetCartAsync(string username, CancellationToken cancellationToken = default) {
            var cart = await session.LoadAsync<ShoppingCart>(username, cancellationToken);
            return cart is null ? throw new CartNotFoundException(username) : cart;
        }

        public async Task<ShoppingCart> StoreCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default) {
            session.Store(cart);
            await session.SaveChangesAsync(cancellationToken);
            return cart;
        }

        public async Task<bool> DeleteCartAsync(string username, CancellationToken cancellationToken = default) {
            session.Delete<ShoppingCart>(username);
            await session.SaveChangesAsync(cancellationToken);
            return true;
        }
    }
}
