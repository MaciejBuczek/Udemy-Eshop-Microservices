namespace Ordering.Application.Orders.Queries.GetOrders
{
    public class GetOrdersQueryHandler(IAppDbContext dbContext) : IQueryHandler<GetOrdersQuery, GetOrdersQueryResult>
    {
        public async Task<GetOrdersQueryResult> Handle(GetOrdersQuery query, CancellationToken cancellationToken)
        {
            var orders = await dbContext.Orders
                .Include(o => o.OrderItems)
                .AsNoTracking()
                .Skip(query.PaginationRequest.PageSize * query.PaginationRequest.PageIndex)
                .Take(query.PaginationRequest.PageSize)
                .ToListAsync(cancellationToken);

            var totalCount = await dbContext.Orders.LongCountAsync(cancellationToken);

            var orderDTOs = orders.Select(o => DomainModelParser.ParseOrderDTO(o));

            return new GetOrdersQueryResult(
                new PaginatedResult<OrderDTO>(
                    query.PaginationRequest.PageIndex,
                    query.PaginationRequest.PageSize,
                    totalCount,
                    orderDTOs));
        }
    }
}
