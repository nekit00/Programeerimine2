using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using KooliProjekt.Application.Data;
using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace KooliProjekt.Application.Features.Products
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, OperationResult<ProductDetailsDto>>
    {
        private readonly ApplicationDbContext _dbContext;

        public GetProductQueryHandler(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<OperationResult<ProductDetailsDto>> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            var result = new OperationResult<ProductDetailsDto>();

            result.Value = await _dbContext.Products
                .Where(p => p.Id == request.Id)
                .Select(p => new ProductDetailsDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Description = p.Description,
                    Photo = p.Photo,
                    Price = p.Price,
                    ClassId = p.ClassId,
                    ClassName = p.Class.Name
                })
                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }
    }
}
