using FluentValidation;

namespace KooliProjekt.Application.Features.Orders
{
    public class GetOrderQueryValidator : AbstractValidator<GetOrderQuery>
    {
        public GetOrderQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
