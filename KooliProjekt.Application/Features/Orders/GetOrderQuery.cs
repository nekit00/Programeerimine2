using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public class GetOrderQuery : IRequest<OperationResult<OrderDetailsDto>>
    {
        public int Id { get; set; }
    }
}
