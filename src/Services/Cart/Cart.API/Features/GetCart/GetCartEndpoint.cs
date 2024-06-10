namespace Cart.API.Features.GetCart
{
    public record GetCartRequest(string Username) : IRequest<GetCartResponse>;

    public record GetCartResponse(ShoppingCart Cart);

    public class GetCartEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/cart/{username}", async (ISender sender, string username) =>
            {
                var result = await sender.Send(new GetCartQuery(username));

                var response = result.Adapt<GetCartResponse>();

                return Results.Ok(response);
            })
                .WithName("GetCart")
                .WithSummary("Get cart by username")
                .WithDescription("Get cart by username")
                .Produces<GetCartResponse>(StatusCodes.Status200OK)
                .ProducesProblem(StatusCodes.Status404NotFound)
                .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
