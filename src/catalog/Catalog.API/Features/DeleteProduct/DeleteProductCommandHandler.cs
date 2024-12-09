namespace Catalog.API.Features.DeleteProduct
{
    public record DeleteProductResult(bool Succeded);
    public record DeleteProductCommand(Guid Id) : ICommand<DeleteProductResult>;

    internal class DeleteProductCommandHandler(IDocumentSession session, ILogger<DeleteProductCommandHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand query, CancellationToken cancellationToken)
        {
            logger.LogInformation("{@Name} called with {@Query}", nameof(DeleteProductCommandHandler), query);

            var product = await session.LoadAsync<Product>(query.Id, cancellationToken) ?? throw new ProductNotFoundException(query.Id);
            session.Delete(product);
            await session.SaveChangesAsync(cancellationToken);

            return new DeleteProductResult(true);
        }
    }
}
