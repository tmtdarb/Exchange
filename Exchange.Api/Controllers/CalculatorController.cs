using Exchange.Application.DTO;
using Exchange.Application.Interfaces;
using Exchange.Application.Responses;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Exchange.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculatorController : ControllerBase
    {
        private readonly ICalculatorService _calculatorService;
        public CalculatorController(ICalculatorService calculatorService)
        {
            _calculatorService = calculatorService;
        }

        // get exchange rate
        [HttpPost("rate")]
        public async Task<IActionResult> GetExchangeRate([FromBody] CalculateExchangeRateRequestModel model)
        {
            var result = await _calculatorService.CalculateExchangeRate(model.CurrencyCodeUserWantToGet.ToUpper(), model.CurrencyCodeUserWantToSell.ToUpper());
            return Ok(ApiResponse<decimal>.SuccessResponse(result,"გაცვლითი კურსი ამ ვალუტებისთვის წარმატებით დაბრუნდა"));
        }
        // get amount from exchange rate
        [HttpPost("amount")]
        public async Task<IActionResult> GetExchangeAmount([FromBody] CalculateAmountRequestModel model)
        {
            var result = await _calculatorService.CalculateAmountFromExchangeRate(model.CurrencyCodeUserWantToGet.ToUpper(), model.CurrencyCodeUserWantToSell.ToUpper(), model.AmountToGet);
            return Ok(ApiResponse<CalculateAmountModel>.SuccessResponse(result, "გასაცემი თანხის ოდენობა წარმატებით დაბრუნდა"));
        }
    }
}
