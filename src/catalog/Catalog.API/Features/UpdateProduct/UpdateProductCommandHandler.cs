namespace Catalog.API.Features.UpdateProduct
{
    public record UpdateProductCommand(Guid Id, string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
        : ICommand<UpdateProductResult>;
    public record UpdateProductResult(bool Succeded);

    public class UpdateProductCommandHandler(IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
        : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            logger.LogInformation("{@Name} called with {@Query}", nameof(UpdateProductCommandHandler), command);

            var product = await session.LoadAsync<Product>(command.Id, cancellationToken)
                ?? throw new ProductNotFoundException();
            
            product.Name = command.Name;
            product.Categories = command.Categories;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageFile = command.ImageFile;

            session.Update<Product>(product);
            await session.SaveChangesAsync(cancellationToken);

            return new UpdateProductResult(true);
        }
    }
}
