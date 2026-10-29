using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Products
{
    public class ListProductsQueryHandler : IRequestHandler<ListProductsQuery, OperationResult<PagedResult<ProductDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListProductsQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<ProductDto>>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<ProductDto>>();

            var query = _dbContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(p => p.Name.Contains(request.Name));
            }

            if (request.ClassId.HasValue)
            {
                query = query.Where(p => p.ClassId == request.ClassId.Value);
            }

            result.Value = await query
                .OrderBy(p => p.Name)
                .Select(p => new ProductDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Photo = p.Photo,
                    Price = p.Price,
                    ClassId = p.ClassId,
                    ClassName = p.Class.Name
                })
                .GetPagedAsync(request.Page, request.PageSize, cancellationToken);

            return result;
        }
    }
}
