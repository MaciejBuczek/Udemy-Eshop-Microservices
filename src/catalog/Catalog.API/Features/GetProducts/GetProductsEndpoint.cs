
using Catalog.API.Features.CreateProduct;

namespace Catalog.API.Features.GetProducts
{
    public record GetProductsResponse(IEnumerable<Product> Products);

    public record GetProductResponse(Product Product);

    public class GetProductsEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender sender) =>
            {
                var result = await sender.Send(new GetProductsQuery());

                var response = result.Adapt<GetProductsResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .Produces<CreateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Products")
            .WithDescription("Get Products");

            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductByIdQuery(id));

                var response = result.Adapt<GetProductResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProduct")
            .Produces<CreateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Product")
            .WithDescription("Get Product By Id");
        }
    }
}
