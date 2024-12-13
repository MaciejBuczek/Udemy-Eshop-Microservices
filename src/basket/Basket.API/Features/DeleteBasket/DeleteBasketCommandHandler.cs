namespace Basket.API.Features.DeleteBasket
{
    record DeleteBasketResult(bool Success);
    record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

    internal class DeleteBasketCommandHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var result = await repository.DeleteBasket(request.UserName, cancellationToken);
            return new DeleteBasketResult(result);
        }
    }
}