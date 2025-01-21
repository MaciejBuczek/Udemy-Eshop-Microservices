namespace Ordering.Application.Orders.Queries.GetOrdersByCustomer
{
    public record GetOrdersByCustomerQueryResult(IEnumerable<OrderDTO> Orders);
    public record GetOrdersByCustomerQuery(Guid CustomerId) : IQuery<GetOrdersByCustomerQueryResult>;

    public class GetORdersByCustomerValidator : AbstractValidator<GetOrdersByCustomerQuery>
    {
        public GetORdersByCustomerValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty();
        }
    }
}
