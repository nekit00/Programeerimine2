using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Customers
{
    public class ListCustomersQuery : IRequest<OperationResult<PagedResult<CustomerDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Valikuline: otsing nime või e-posti järgi
        public string Search { get; set; }
    }
}
