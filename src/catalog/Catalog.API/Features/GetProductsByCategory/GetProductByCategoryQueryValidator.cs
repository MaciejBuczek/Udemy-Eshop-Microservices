namespace Catalog.API.Features.GetProductsByCategory
{
    public class GetProductByCategoryQueryValidator : AbstractValidator<GetProductByCategoryQuery>
    {
        public GetProductByCategoryQueryValidator()
        {
            RuleFor(x => x.Category).NotEmpty();
        }
    }
}
