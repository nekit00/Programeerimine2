using FluentValidation;

namespace KooliProjekt.Application.Features.Customers
{
    public class GetCustomerQueryValidator : AbstractValidator<GetCustomerQuery>
    {
        public GetCustomerQueryValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0);
        }
    }
}
