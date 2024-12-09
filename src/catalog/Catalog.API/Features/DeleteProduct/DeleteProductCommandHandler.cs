namespace Catalog.API.Features.DeleteProduct
{
    public record DeleteProductResult(bool Succeded);
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    internal class DeleteProductCommandHandler(IDocumentSession session) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand query, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(query.Id, cancellationToken) ?? throw new ProductNotFoundException(query.Id);
            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
