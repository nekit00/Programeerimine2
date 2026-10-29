using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.OrderLines
{
    public class GetOrderLineQueryHandler : IRequestHandler<GetOrderLineQuery, OperationResult<OrderLineDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetOrderLineQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<OrderLineDto>> Handle(GetOrderLineQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<OrderLineDto>();

            result.Value = await _dbContext.OrderLines
                .Where(l => l.Id == request.Id)
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
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
