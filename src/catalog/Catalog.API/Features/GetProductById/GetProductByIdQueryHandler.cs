namespace Catalog.API.Features.GetProductById
{
    public record GetProductResult(Product Product);
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductResult>;

    internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> logger) : IQueryHandler<GetProductByIdQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("{@Name} called with {@Query}", nameof(GetProductResult), query);

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (product != null)
            {
                return new GetProductResult(product);
            }

            throw new ProductNotFoundException();
        }
    }
}
