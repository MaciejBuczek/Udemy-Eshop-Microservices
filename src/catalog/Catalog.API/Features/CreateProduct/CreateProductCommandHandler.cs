using Catalog.API.Models;
using Common.CQRS;
using Marten;

namespace Catalog.API.Features.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult (Guid Id);

    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand commmand, CancellationToken cancellationToken)
        {
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
