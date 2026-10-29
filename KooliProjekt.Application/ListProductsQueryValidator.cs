using FluentValidation;

namespace KooliProjekt.Application.Features.Products
{
    public class ListProductsQueryValidator : AbstractValidator<ListProductsQuery>
    {
        public ListProductsQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
