namespace Ordering.Application.Orders.Queries.GetOrdersByName
{
    public record GetOrdersByNameQueryResult(IEnumerable<OrderDTO> Orders);
    public record GetOrdersByNameQuery(string Name) : IQuery<GetOrdersByNameQueryResult>;

    public class GetOrderByNameQueryValidator : AbstractValidator<GetOrdersByNameQuery>
    {
        public GetOrderByNameQueryValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
        }
    }
}
