using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public class GetProductQuery : IRequest<OperationResult<ProductDetailsDto>>
    {
        public int Id { get; set; }
    }
}
