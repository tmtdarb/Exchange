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
        [HttpGet("Conversions")]
        public async Task<IActionResult> GetConversions(DateTime from,DateTime to)
        {
            var result = await _mediator.Send(new GetConversionsByDateQuery(from, to));
            return Ok(ApiResponse<List<ConversionModel>>.SuccessResponse(result, "კონვერტაციები მოცემულ თარიღში წარმატებით დაბრუნდა"));
        }
        [HttpGet("SuspiciousConversions")]
        public async Task<IActionResult> GetSuspiciousConversions(DateTime from, DateTime to)
        {
            var result = await _mediator.Send(new GetSuspiciousConversionsByDateQuery(from, to));
            return Ok(ApiResponse<List<ConversionModel>>.SuccessResponse(result, "საეჭვო კონვერტაციები მოცემულ თარიღში წარმატებით დაბრუნდა"));
        }
    }
}
