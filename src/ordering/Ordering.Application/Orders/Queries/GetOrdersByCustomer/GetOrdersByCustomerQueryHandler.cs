namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public class GetOrdersByCustomerQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetOrdersByCustomerQuery, GetOrdersByCustomerQueryResult>
    {
        public async Task<GetOrdersByCustomerQueryResult> Handle(GetOrdersByCustomerQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Where(o => o.CustomerId == CustomerId.Of(query.CustomerId))
                .OrderBy(o => o.OrderName.Name)
                .ToListAsync(cancellationToken);

            var orderDTOs = orders.Select(o => DomainModelParser.ParseOrderDTO(o));

            return new GetOrdersByCustomerQueryResult(orderDTOs);
        }
    }
}