using FluentValidation;

namespace KooliProjekt.Application.Features.Orders
{
    public class ListOrdersQueryValidator : AbstractValidator<ListOrdersQuery>
    {
        public ListOrdersQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
