using Exchange.Application.CQRS.Reports.Queries;
using Exchange.Application.DTO;
using Exchange.Application.Responses;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> Get(DateTime from,DateTime to)
        {
            var result = await _mediator.Send(new GetConversionsByDateQuery(from, to));
            return Ok(ApiResponse<List<ConversionModel>>.SuccessResponse(result, "კონვერტაციები მოცემულ თარიღში წარმატებით დაბრუნდა"));
        }
    }
}
