namespace Basket.API.Features.DeleteBasket
{
    public record DeleteBasketResult(bool Success);
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

    internal class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteBasket(request.UserName, cancellationToken);
            return new DeleteBasketResult(result);
        }
    }
}