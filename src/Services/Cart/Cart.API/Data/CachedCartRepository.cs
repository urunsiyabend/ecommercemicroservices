
using System.Text.Json;

namespace Cart.API.Data
{
    public class CachedCartRepository(ICartRepository cartRepository, IDistributedCache distributedCache) : ICartRepository
    {
        private static string GenerateCacheKey(string username)
        {
            return String.Format("cart#{0}", username).ToLowerInvariant();
        }

        public async Task<ShoppingCart> GetCartAsync(string username, CancellationToken cancellationToken = default)
        {
            var cacheKey = GenerateCacheKey(username);
            var cachedCart = await distributedCache.GetStringAsync(cacheKey, cancellationToken);
            if (!string.IsNullOrEmpty(cachedCart))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedCart)!;
            }

            var cart = await cartRepository.GetCartAsync(username, cancellationToken);
            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(cart), cancellationToken);
            return cart;
        }

        public async Task<ShoppingCart> StoreCartAsync(ShoppingCart cart, CancellationToken cancellationToken = default)
        {
            await cartRepository.StoreCartAsync(cart, cancellationToken);
            var cacheKey = GenerateCacheKey(cart.Username);
            await distributedCache.SetStringAsync(cacheKey, JsonSerializer.Serialize(cart), cancellationToken);
            return cart;

        }

        public async Task<bool> DeleteCartAsync(string username, CancellationToken cancellationToken = default)
        {
            await cartRepository.DeleteCartAsync(username, cancellationToken);
            var cacheKey = GenerateCacheKey(username);
            await distributedCache.RemoveAsync(cacheKey, cancellationToken);
            return true;
        }
    }
}
