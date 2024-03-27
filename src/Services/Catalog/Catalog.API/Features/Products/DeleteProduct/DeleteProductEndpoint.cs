
namespace Catalog.API.Features.Products.DeleteProduct
{
    //public record DeleteProductRequest(Guid ID) : ICommand;

    //public record DeleteProductResponse;

    public class DeleteProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (ISender sender, Guid id) =>
            {
                await sender.Send(new DeleteProductCommand(id));

                return Results.NoContent();
            })
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
