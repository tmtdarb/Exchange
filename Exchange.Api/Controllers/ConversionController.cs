using Exchange.Application.CQRS.Conversions.Commands;
using Exchange.Application.CQRS.Conversions.Queries;
using Exchange.Application.CQRS.Currencies.Queries;
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
    public class ConversionController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ConversionController(IMediator mediator)
        {
            _mediator = mediator;
        }
        // make conversion
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateConversionModel model)
        {
            var result = await _mediator.Send(new CreateConversionCommand(model));
            return Ok(ApiResponse<ConversionModel>.SuccessResponse(result, "კონვერტაცია წარმატებით შესრულდა"));
        }
        // get all conversions
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _mediator.Send(new GetAllConversionsQuery());
            return Ok(ApiResponse<List<ConversionModel>>.SuccessResponse(result, "კონვერტაციები წარმატებით დაბრუნდა"));
        }
    }
}
