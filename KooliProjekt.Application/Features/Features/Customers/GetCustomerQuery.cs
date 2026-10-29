using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Customers
{
    public class GetCustomerQuery : IRequest<OperationResult<CustomerDetailsDto>>
    {
        public int Id { get; set; }
    }
}
