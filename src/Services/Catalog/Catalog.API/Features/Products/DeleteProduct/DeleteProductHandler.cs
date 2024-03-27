
namespace Catalog.API.Features.Products.DeleteProduct
{
    public record DeleteProductCommand(Guid ID) : ICommand;

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.ID).NotEmpty();
        }
    }

    internal class DeleteProductCommandHandler
        (IDocumentSession session)
        : ICommandHandler<DeleteProductCommand>
    {
        public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.ID, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(request.ID);
            }

            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
    }
}
