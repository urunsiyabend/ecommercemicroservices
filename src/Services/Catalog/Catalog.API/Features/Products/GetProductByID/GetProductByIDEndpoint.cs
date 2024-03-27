
namespace Catalog.API.Features.Products.GetProductByID
{
    //public record GetProductByIDQuery(Guid ID) : IQuery<GetProductByIDResult>;

    public record GetProductByIDResponse(Product Product);

    public class GetProductByIDEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (ISender sender, Guid id) =>
            {
                var result = await sender.Send(new GetProductByIDQuery(id));

                var response = result.Adapt<GetProductByIDResponse>();

                return Results.Ok(response);
            })
            .Produces<GetProductByIDResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
