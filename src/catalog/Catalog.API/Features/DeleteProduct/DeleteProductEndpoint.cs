﻿namespace Catalog.API.Features.DeleteProduct
{
    public class DeleteProduct : ICarterModule
    {
        public record DeleteProductResponse(bool Succeded);

        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new DeleteProductCommand(id));

                var response = result.Adapt<DeleteProductResponse>();
                return Results.Ok(response);
            })
            .WithName("Delete Product")
            .Produces<DeleteProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete Product")
            .WithDescription("Delete Product By Id");
        }
    }
}