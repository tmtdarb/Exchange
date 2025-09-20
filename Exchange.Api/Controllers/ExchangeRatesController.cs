using Exchange.Application.CQRS.ExchangeRates.Commands;
using Exchange.Application.CQRS.ExchangeRates.Queries;
using Exchange.Application.DTO;
using Exchange.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExchangeRatesController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ExchangeRatesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // get all exchange rate
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllLatestExchangeRateQuery());
            return Ok(ApiResponse<List<ExchangeRateModel>>.SuccessResponse(result,"ვალუტის კურსები წარმატებით დაბრუნდა"));
        }

        // get exchange rate by id
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _mediator.Send(new GetExchangeRateByIdQuery(id));
            return Ok(ApiResponse<ExchangeRateModel>.SuccessResponse(result, "ვალუტის კურსი წარმატებით დაბრუნდა"));
        }

        // create exchange rate
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateExchangeRateModel model)
        {
            var result = await _mediator.Send(new CreateExchangeRateCommand(model));
            return Ok(ApiResponse<ExchangeRateModel>.SuccessResponse(result, "ვალუტის კურსი წარმატებით დაემატა"));
        }

        // update exchange rate
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] CreateExchangeRateModel model)
        {
            var result = await _mediator.Send(new UpdateExchangeRateCommand(id, model));
            return Ok(ApiResponse<ExchangeRateModel>.SuccessResponse(result, "ვალუტი კურსი წარმატებით განახლდა"));
        }

        // delete exchange rate
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteExchangeRateCommand(id));
            return Ok(ApiResponse<bool>.SuccessResponse(true, "ვალუტის კურსი წარმატებით წაიშალა"));
        }
    }
}
