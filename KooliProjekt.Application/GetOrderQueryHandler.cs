using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Orders
{
    public class GetOrderQueryHandler : IRequestHandler<GetOrderQuery, OperationResult<OrderDetailsDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetOrderQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<OrderDetailsDto>> Handle(GetOrderQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<OrderDetailsDto>();

            var order = await _dbContext.Orders
                .Where(o => o.Id == request.Id)
                .Select(o => new OrderDetailsDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    CustomerId = o.CustomerId,
                    CustomerName = o.Customer.Name,
                    CustomerEmail = o.Customer.Email
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (order == null)
            {
                // Value jääb null -> kontroller tagastab 404
                return result;
            }

            order.Lines = await _dbContext.OrderLines
                .Where(l => l.OrderId == order.Id)
                .OrderBy(l => l.Id)
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
                .ToListAsync(cancellationToken);

            order.ItemCount = order.Lines.Sum(l => l.Quantity);
            order.Total = order.Lines.Sum(l => l.LineTotal);

            result.Value = order;

            return result;
        }
    }
}
