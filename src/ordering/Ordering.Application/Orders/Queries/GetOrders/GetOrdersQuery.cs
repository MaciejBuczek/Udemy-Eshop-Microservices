namespace Ordering.Application.Orders.Queries.GetOrders
{
    public record GetOrdersQueryResult(PaginatedResult<OrderDTO> Orders);
    public record GetOrdersQuery(PaginationRequest PaginationRequest) : IQuery<GetOrdersQueryResult>;

    public class GetOrdersQueryValidator : AbstractValidator<GetOrdersQuery>
    {
        public GetOrdersQueryValidator()
        {
            RuleFor(x => x.PaginationRequest.PageIndex).GreaterThanOrEqualTo(0);
            RuleFor(x => x.PaginationRequest.PageSize).GreaterThanOrEqualTo(1);
        }
    }
}