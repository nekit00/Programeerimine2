using KooliProjekt.Application.Dto;
using KooliProjekt.Application.Infrastructure.Results;
using MediatR;

namespace KooliProjekt.Application.Features.Classes
{
    public class GetClassQuery : IRequest<OperationResult<ClassDetailsDto>>
    {
        public int Id { get; set; }
    }
}
