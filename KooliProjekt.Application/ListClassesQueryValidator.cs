using FluentValidation;

namespace KooliProjekt.Application.Features.Classes
{
    public class ListClassesQueryValidator : AbstractValidator<ListClassesQuery>
    {
        public ListClassesQueryValidator()
        {
            RuleFor(x => x.Page).GreaterThan(0);
            RuleFor(x => x.PageSize).InclusiveBetween(1, 100);
        }
    }
}
