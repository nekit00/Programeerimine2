using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Classes
{
    public class GetClassQueryHandler : IRequestHandler<GetClassQuery, OperationResult<ClassDetailsDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetClassQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<ClassDetailsDto>> Handle(GetClassQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ClassDetailsDto>();

            result.Value = await _dbContext.Classes
                .Where(c => c.Id == request.Id)
                .Select(c => new ClassDetailsDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Products = _dbContext.Products
                        .Where(p => p.ClassId == c.Id)
                        .OrderBy(p => p.Name)
                        .Select(p => new ProductDto
                        {
                            Id = p.Id,
                            Name = p.Name,
                            Photo = p.Photo,
                            Price = p.Price,
                            ClassId = c.Id,
                            ClassName = c.Name
                        })
                        .ToList()
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
