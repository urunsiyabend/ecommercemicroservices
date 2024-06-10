
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

    public class StoreCartCommandHandler(ICartRepository cartRepository) : ICommandHandler<StoreCartCommand, StoreCartResult>
    {
        public async Task<StoreCartResult> Handle(StoreCartCommand command, CancellationToken cancellationToken)
        {
            ShoppingCart cart = command.Cart;

            await cartRepository.StoreCartAsync(cart, cancellationToken);

            return new StoreCartResult(cart.Username);
        }
    }
}
