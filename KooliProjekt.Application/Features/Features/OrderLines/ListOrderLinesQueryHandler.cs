using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.OrderLines
{
    public class ListOrderLinesQueryHandler : IRequestHandler<ListOrderLinesQuery, OperationResult<PagedResult<OrderLineDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListOrderLinesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<OrderLineDto>>> Handle(ListOrderLinesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<OrderLineDto>>();

            var query = _dbContext.OrderLines.AsQueryable();

            if (request.OrderId.HasValue)
            {
                query = query.Where(l => l.OrderId == request.OrderId.Value);
            }

            result.Value = await query
                .OrderBy(l => l.OrderId)
                .ThenBy(l => l.Id)
                .Select(l => new OrderLineDto
                {
                    Id = l.Id,
                    OrderId = l.OrderId,
                    ProductId = l.ProductId,
                    ProductName = l.Product.Name,
                    ProductPhoto = l.Product.Photo,
                    Quantity = l.Quantity,
                    UnitPrice = l.UnitPrice,
                    LineTotal = l.Quantity * l.UnitPrice
                })
                .GetPagedAsync(request.Page, request.PageSize, cancellationToken);

            return result;
        }
    }
}
