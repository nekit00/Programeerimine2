using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Classes
{
    public class ListClassesQueryHandler : IRequestHandler<ListClassesQuery, OperationResult<PagedResult<ClassDto>>>
    {
        private readonly ApplicationDbContext _dbContext;

        public ListClassesQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<PagedResult<ClassDto>>> Handle(ListClassesQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<PagedResult<ClassDto>>();

            var query = _dbContext.Classes.AsQueryable();

            if (!string.IsNullOrWhiteSpace(request.Name))
            {
                query = query.Where(c => c.Name.Contains(request.Name));
            }

            result.Value = await query
                .OrderBy(c => c.Name)
                .Select(c => new ClassDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    ProductCount = _dbContext.Products.Count(p => p.ClassId == c.Id)
                })
                .GetPagedAsync(request.Page, request.PageSize, cancellationToken);

            return result;
        }
    }
}
