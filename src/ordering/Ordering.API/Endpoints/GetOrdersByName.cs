namespace Ordering.API.Endpoints
{
    public record GetOrdersByNameResponse(IEnumerable<OrderDTO> orders);

    public class GetOrdersByName : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orderrs/{orderName}", async (string orderName, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersByNameQuery(orderName));
                var response = result.Adapt<GetOrdersByNameResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrdersByName")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders By Name")
            .WithDescription("Get Orders By Order Name");
        }
    }
}
