namespace Cart.API.Features.StoreCart
{
    public record StoreCartRequest(ShoppingCart Cart) : IRequest<StoreCartResponse>;

    public record StoreCartResponse(string Username);

    public class StoreCartEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/cart", async (ISender sender, StoreCartRequest request) =>
            {
                var result = await sender.Send(new StoreCartCommand(request.Cart));

                var response = result.Adapt<StoreCartResponse>();

                return Results.Created($"/cart/{response.Username}", response);
            })
            .WithName("StoreCart")
            .WithSummary("Stores a shopping cart")
            .WithDescription("Stores a shopping cart in the cart service")
            .Produces<StoreCartResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
