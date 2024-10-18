using Catalog.API.Models;
using Common.CQRS;

namespace Catalog.API.Features.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Categories, string Description, string ImageFile, decimal Price)
        : ICommand<CreateProductResult>;

    public record CreateProductResult (Guid Id);

    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
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

            return new CreateProductResult(Guid.NewGuid());
        }
    }
}
