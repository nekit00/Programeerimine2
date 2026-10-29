using System.Threading.Tasks;
using KooliProjekt.Application.Features.OrderLines;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace KooliProjekt.WebAPI.Controllers
{
    public class OrderLinesController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public OrderLinesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Route("List")]
        public async Task<IActionResult> List([FromQuery] ListOrderLinesQuery query)
        {
            var response = await _mediator.Send(query);

            return Result(response);
        }

        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get(int id)
        {
            var query = new GetOrderLineQuery { Id = id };
            var response = await _mediator.Send(query);

            return Result(response);
        }
    }
}
