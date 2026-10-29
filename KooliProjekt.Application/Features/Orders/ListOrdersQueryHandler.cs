using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Orders
{
    public class ListOrdersQueryHandler : IRequestHandler<ListOrdersQuery, OperationResult<PagedResult<OrderDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListOrdersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<OrderDto>>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<OrderDto>>();

            var query = _dbContext.Orders.AsQueryable();

            if (request.CustomerId.HasValue)
            {
                query = query.Where(o => o.CustomerId == request.CustomerId.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.Status))
            {
                query = query.Where(o => o.Status == request.Status);
            }

            // Uuemad tellimused eespool
            result.Value = await query
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer.Name,
                    ItemCount = _dbContext.OrderLines.Where(l => l.OrderId == o.Id).Sum(l => l.Quantity),
                    Total = _dbContext.OrderLines.Where(l => l.OrderId == o.Id).Sum(l => l.Quantity * l.UnitPrice)
                })
                .GetPagedAsync(request.Page, request.PageSize, cancellationToken);

            return result;
        }
    }
}
