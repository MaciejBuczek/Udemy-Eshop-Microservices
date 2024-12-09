namespace Catalog.API.Features.GetProductById
{
    public record GetProductResult(Product Product);
    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductResult>;

    internal class GetProductByIdQueryHandler(IDocumentSession session) : IQueryHandler<GetProductByIdQuery, GetProductResult>
    {
        public async Task<GetProductResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken);

            if (product != null)
            {
                return new GetProductResult(product);
            }

            throw new ProductNotFoundException(query.Id);
        }
    }
}
