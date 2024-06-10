namespace Cart.API.Features.DeleteCart
{
    //public record DeleteCartRequest(string Username) : IRequest<DeleteCartResponse>;

    //public record DeleteCartResponse;

    public class DeleteCartEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/cart/{username}", async (ISender sender, string username) =>
            {
                await sender.Send(new DeleteCartCommand(username));

                return Results.NoContent();
            })
            .WithName("DeleteCart")
            .WithSummary("Deletes a cart")
            .WithDescription("Deletes a cart of a user")
            .Produces(StatusCodes.Status204NoContent)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
