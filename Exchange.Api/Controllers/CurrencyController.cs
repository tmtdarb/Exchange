using Exchange.Application.CQRS.Currencies.Commands;
using Exchange.Application.CQRS.Currencies.Queries;
using Exchange.Application.DTO;
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
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMediator _mediator;
        public CurrencyController(ICurrencyRepository currencyRepository, IMediator mediator)
        {
            _currencyRepository = currencyRepository;
            _mediator = mediator;
        }
        // get all active currencies
        [HttpGet]
        public async Task<List<CurrencyModel>> Get()
        {
            return await _mediator.Send(new GetAllActiveCurenciesQuery());
        }

        // create new currency
        [HttpPost]
        public async Task Post([FromBody] CurrencyModel model)
        {
            await _mediator.Send(new CreateCurrencyCommand(model));
        }

        // edit existing currency
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] CurrencyModel model)
        {
            model.ID = id;
            await _mediator.Send(new UpdateCurrencyCommand(model));
        }

        // delete command
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _mediator.Send(new DeleteCurrencyCommand(id));
        }
    }
}
