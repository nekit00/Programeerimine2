using FluentValidation;

namespace KooliProjekt.Application.Features.Classes
{
    public class GetClassQueryValidator : AbstractValidator<GetClassQuery>
    {
        public GetClassQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
