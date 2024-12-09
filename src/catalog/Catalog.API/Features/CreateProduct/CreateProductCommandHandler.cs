using Catalog.API.Features.DeleteProduct;

namespace Catalog.API.Features.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession session, ILogger<CreateProductCommand> logger) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand commmand, CancellationToken cancellationToken)
        {
            logger.LogInformation("{@Name} called with {@Commmand}", nameof(DeleteProductCommandHandler), commmand);

            var product = new Product
            {
                Name = commmand.Name,
                Categories = commmand.Categories,
                Description = commmand.Description,
                ImageFile = commmand.ImageFile,
                Price = commmand.Price
            };

            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);

            return new CreateProductResult(product.Id);
        }
    }
}