using FluentValidation;

namespace KooliProjekt.Application.Features.OrderLines
{
    public class GetOrderLineQueryValidator : AbstractValidator<GetOrderLineQuery>
    {
        public GetOrderLineQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
