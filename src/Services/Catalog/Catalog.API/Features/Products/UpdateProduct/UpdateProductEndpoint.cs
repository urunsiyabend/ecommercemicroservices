
namespace Catalog.API.Features.Products.UpdateProduct
{
    public record UpdateProductRequest(string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record UpdateProductResponse(Guid ID);

    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/products/{id}", async (UpdateProductRequest request, ISender sender, string id) => {
                var command = new UpdateProductCommand(Guid.Parse(id), request.Name, request.Category, request.Description, request.ImageFile, request.Price);

                var result = await sender.Send(command);

                var response = new UpdateProductResponse(result.ID);

                return Results.Ok(response);
            })
                .WithName("UpdateProduct")
                .WithSummary("Update a product")
                .WithDescription("Update a product in the catalog")
                .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status400BadRequest)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
