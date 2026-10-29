using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.OrderLines
{
    public class ListOrderLinesQuery : IRequest<OperationResult<PagedResult<OrderLineDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Valikuline: ainult ühe tellimuse read
        public int? OrderId { get; set; }
    }
}
