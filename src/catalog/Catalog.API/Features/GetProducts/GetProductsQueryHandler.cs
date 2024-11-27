namespace Catalog.API.Features.GetProducts
{
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductResult>;
    public record GetProductResult(Product Product);

    public record GetProductsQuery() : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);

    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger)
        : IQueryHandler<GetProductsQuery, GetProductsResult>, IQueryHandler<GetProductByIdQuery, GetProductResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("{@Name} called with {@Query}", nameof(GetProductsQueryHandler), query);
            
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            
            return new GetProductsResult(products);
        }

        public async Task<GetProductResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation("{@Name} called with {@Query}", nameof(GetProductResult), query);

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if(product != null)
            {
                return new GetProductResult(product);
            }

            throw new ProductNotFoundException();
        }
    }
}
