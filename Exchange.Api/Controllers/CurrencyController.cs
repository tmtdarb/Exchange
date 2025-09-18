using Exchange.Application.CQRS.Currencies.Commands;
using Exchange.Application.CQRS.Currencies.Queries;
using Exchange.Application.DTO;
using Exchange.Application.Responses;
using Exchange.Domain.Entities;
using Exchange.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CurrencyController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // get all active currencies
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllActiveCurenciesQuery());
            return Ok(ApiResponse<List<CurrencyModel>>.SuccessResponse(result, "ვალუტები წარმატებით დაბრუნდა"));
        }

        // create new currency
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCurrencyModel model)
        {
            var result = await _mediator.Send(new CreateCurrencyCommand(model));
            return Ok(ApiResponse<CurrencyModel>.SuccessResponse(result, "ვალუტა წარმატებით შეიქმნა!"));

        }

        // edit existing currency
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreateCurrencyModel model)
        {
            var result = await _mediator.Send(new UpdateCurrencyCommand(model, id));
            return Ok(ApiResponse<CurrencyModel>.SuccessResponse(result, "ვალუტა წარმატებით განახლდა"));
        }

        // delete command
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCurrencyCommand(id));
            return Ok(ApiResponse<bool>.SuccessResponse(true, "ვალუტა წარმატებით წაიშალა"));
        }
    }
}
