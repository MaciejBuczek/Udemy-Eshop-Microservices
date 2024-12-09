namespace Catalog.API.Features.GetProductsByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products) : IQuery<GetProductByCategoryResult>;

    internal class GetProductsByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().Where(p => p.Categories.Contains(query.Category)).ToListAsync(cancellationToken);

            return new GetProductByCategoryResult(products);
        }
    }
}
