using Cart.API.Data;

namespace Cart.API.Features.GetCart
{
    public record GetCartQuery(string Username) : IQuery<GetCartResult>;

    public record GetCartResult(ShoppingCart Cart);

    public class GetCartQueryHandler(ICartRepository cartRepository) : IQueryHandler<GetCartQuery, GetCartResult>
    {
        public async Task<GetCartResult> Handle(GetCartQuery query, CancellationToken cancellationToken)
        {
            var cart = await cartRepository.GetCartAsync(query.Username, cancellationToken);
            return new GetCartResult(cart);
        }
    }
}
