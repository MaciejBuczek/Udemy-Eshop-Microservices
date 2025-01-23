namespace Ordering.API.Endpoints
{
    public record GetOrdersResponse(PaginatedResult<OrderDTO> Orders);
    public class GetOrders : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/orders", async ([AsParameters] PaginationRequest request, ISender sender) =>
            {
                var result = await sender.Send(new GetOrdersQuery(request));
                var response = result.Adapt<GetOrdersResponse>();

                return Results.Ok(response);
            })
            .WithName("GetOrders")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Orders By Customer")
            .WithDescription("Get Orders By Customer Id");
        }
    }
}
