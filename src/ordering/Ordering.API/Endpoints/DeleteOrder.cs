namespace Ordering.API.Endpoints
{
    public record DeleteOrderResponse(bool IsSuccess);
    public record DeleteOrderRequest(Guid Id);
    public class DeleteOrder : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/orders", async (DeleteOrderRequest request, ISender sender) =>
            {
                var command = request.Adapt<DeleteOrderCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<DeleteOrderResponse>();

                return Results.Ok(response);
            })
            .WithName("DeleteOrder")
            .Produces<CreateOrderResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Order")
            .WithDescription("Delete Order");

        }
    }
}
