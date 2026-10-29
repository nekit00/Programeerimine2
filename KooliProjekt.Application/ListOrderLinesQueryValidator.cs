using FluentValidation;

namespace KooliProjekt.Application.Features.OrderLines
{
    public class ListOrderLinesQueryValidator : AbstractValidator<ListOrderLinesQuery>
    {
        public ListOrderLinesQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
