using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Customers
{
    public class GetCustomerQueryHandler : IRequestHandler<GetCustomerQuery, OperationResult<CustomerDetailsDto>>
    {
        private const string CancelledStatus = "Tühistatud";

        private readonly ApplicationDbContext _dbContext;

        public GetCustomerQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<CustomerDetailsDto>> Handle(GetCustomerQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<CustomerDetailsDto>();

            var customer = await _dbContext.Customers
                .Where(c => c.Id == request.Id)
                .Select(c => new CustomerDetailsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email
                })
                .FirstOrDefaultAsync(cancellationToken);

            if (customer == null)
            {
                // Value jääb null -> kontroller tagastab 404
                return result;
            }

            customer.Orders = await _dbContext.Orders
                .Where(o => o.CustomerId == customer.Id)
                .OrderByDescending(o => o.OrderDate)
                .Select(o => new OrderDto
                {
                    Id = o.Id,
                    OrderDate = o.OrderDate,
                    Status = o.Status,
                    CustomerId = o.CustomerId,
                    CustomerName = customer.Name,
                    ItemCount = _dbContext.OrderLines.Where(l => l.OrderId == o.Id).Sum(l => l.Quantity),
                    Total = _dbContext.OrderLines.Where(l => l.OrderId == o.Id).Sum(l => l.Quantity * l.UnitPrice)
                })
                .ToListAsync(cancellationToken);

            customer.OrderCount = customer.Orders.Count;

            // Tühistatud tellimusi kulutatud summa hulka ei arvesta
            customer.TotalSpent = customer.Orders
                .Where(o => o.Status != CancelledStatus)
                .Sum(o => o.Total);

            result.Value = customer;

            return result;
        }
    }
}
