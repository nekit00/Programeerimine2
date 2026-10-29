using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Customers
{
    public class ListCustomersQueryHandler : IRequestHandler<ListCustomersQuery, OperationResult<PagedResult<CustomerDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListCustomersQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<CustomerDto>>> Handle(ListCustomersQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<CustomerDto>>();

            var query = _dbContext.Customers.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                query = query.Where(c => c.Name.Contains(request.Search) || c.Email.Contains(request.Search));
            }

            result.Value = await query
                .OrderBy(c => c.Name)
                .Select(c => new CustomerDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Email = c.Email,
                    OrderCount = _dbContext.Orders.Count(o => o.CustomerId == c.Id)
                })
                .GetPagedAsync(request.Page, request.PageSize, cancellationToken);

            return result;
        }
    }
}
