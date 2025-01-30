namespace Ordering.Application.Orders.EventHandlers.Integration
{
    public class BasketCheckoutEventHandler(ISender sender, ILogger<BasketCheckoutEventHandler> logger) 
        : IConsumer<BasketCheckoutEvent>
    {
        public async Task Consume(ConsumeContext<BasketCheckoutEvent> context)
        {
            logger.LogInformation("Integration Event handlerd: {IntegrationEvent}", context.Message.GetType().Name);
            var command = EventMapper.MapToCreateOrderCommand(context.Message);
            await sender.Send(command);
        }
    }
}
