using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public class ListProductsQuery : IRequest<OperationResult<PagedResult<ProductDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Valikulised filtrid
        public string Name { get; set; }
        public int? ClassId { get; set; }
    }
}
