namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger, IPublishEndpoint publishEndpoint, IFeatureManager featureManager)
        : INotificationHandler<OrderCreatedEvent>
    {
        public async Task Handle(OrderCreatedEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain Event handled: {DomainEvent}", domainEvent.GetType());

            if(await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIntegrationEvent = DomainModelParser.ParseOrderDTO(domainEvent.Order);
                await publishEndpoint.Publish(orderCreatedIntegrationEvent, cancellationToken);
            }
        }
    }
}
