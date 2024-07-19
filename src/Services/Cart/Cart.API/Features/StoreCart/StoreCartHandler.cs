namespace Cart.API.Features.StoreCart
{
    public record StoreCartCommand(ShoppingCart Cart) : ICommand<StoreCartResult>;

    public record StoreCartResult(string Username);

    public class StoreCartCommandValidator : AbstractValidator<StoreCartCommand>
    {
        public StoreCartCommandValidator()
        {
            RuleFor(x => x.Cart)
                .NotNull().WithMessage("Cart is required");

            RuleFor(x => x.Cart.Username)
                .NotNull().WithMessage("Username is required")
                .NotEmpty().WithMessage("Username is required");
        }
    }

    public class StoreCartCommandHandler(ICartRepository cartRepository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreCartCommand, StoreCartResult>
    {
        public async Task<StoreCartResult> Handle(StoreCartCommand command, CancellationToken cancellationToken)
        {
            await DeductDiscount(command.Cart, cancellationToken);
            
            ShoppingCart cart = command.Cart;

            await cartRepository.StoreCartAsync(cart, cancellationToken);

            return new StoreCartResult(cart.Username);
        }

        public async Task DeductDiscount(ShoppingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var discountRequest = new GetDiscountRequest { ProductId = item.ProductId.ToString() };
                var discount = await discountProto.GetDiscountAsync(discountRequest);

                if (discount.Amount > 0)
                {
                    item.Price -= (decimal)discount.Amount;
                }
            }
        }
    }
}
