using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Paging;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Classes
{
    public class ListClassesQuery : IRequest<OperationResult<PagedResult<ClassDto>>>
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

        // Valikuline: otsing nime järgi
        public string Name { get; set; }
    }
}
