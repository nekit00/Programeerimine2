using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.OrderLines
{
    public class GetOrderLineQuery : IRequest<OperationResult<OrderLineDto>>
    {
        public int Id { get; set; }
    }
}
