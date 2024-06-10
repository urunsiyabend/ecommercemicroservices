namespace Cart.API.Exceptions
{
    public class CartNotFoundException : NotFoundException
    {
        public CartNotFoundException(string username) : base("Cart", username)
        {
        }
    }
}
