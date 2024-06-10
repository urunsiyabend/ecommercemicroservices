
namespace Cart.API.Features.DeleteCart
{
    public record DeleteCartCommand(string Username) : IRequest<DeleteCartResult>;

    public record DeleteCartResult;

    public class DeleteCartCommandValidator : AbstractValidator<DeleteCartCommand>
    {
        public DeleteCartCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required");
        }
    }

    internal class DeleteCartHandler(ICartRepository cartRepository) : IRequestHandler<DeleteCartCommand, DeleteCartResult>
    {
        public async Task<DeleteCartResult> Handle(DeleteCartCommand command, CancellationToken cancellationToken)
        {
            await cartRepository.DeleteCartAsync(command.Username, cancellationToken);
            
            return new DeleteCartResult();
        }
    }
}
