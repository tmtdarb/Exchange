using AutoMapper;
using Exchange.Application.DTO;
using Exchange.Domain.Entities;
using Exchange.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.CQRS.ExchangeRates.Commands
{
    public record CreateExchangeRateCommand(CreateExchangeRateModel model) : IRequest<ExchangeRateModel>;
    public class CreateExchangeRateCommandHandler : IRequestHandler<CreateExchangeRateCommand, ExchangeRateModel>
    {
        private readonly IMapper _mapper;
        private readonly IExchangeRatesRepository _repository;
        private readonly ICurrencyRepository _currencyRepo;
        public CreateExchangeRateCommandHandler(IMapper mapper, IExchangeRatesRepository repository, ICurrencyRepository currencyRepo)
        {
            _mapper = mapper;
            _repository = repository;
            _currencyRepo = currencyRepo;
        }
        public async Task<ExchangeRateModel> Handle(CreateExchangeRateCommand request, CancellationToken cancellationToken)
        {
            var exchangeModel = _mapper.Map<ExchangeRate>(request.model);
            var activeCurrencies = (await _currencyRepo.GetAllActiveCurrencies()).Where(a => a.CurrencyCode.ToUpper() != "GEL");
            var currencyExist = activeCurrencies.Any(a => a.ID == request.model.CurrencyID);
            if (exchangeModel is null || !currencyExist)
                throw new InvalidOperationException("ასეთი ვალუტა, რის მიმართაც იქმნება გაცვლითი კურსი, არ არსებობს");
            var gelCurency = await _currencyRepo.GetCurrencyByCode("gel");
            if(exchangeModel.CurrencyID == gelCurency.ID)
                throw new InvalidOperationException("ვალუტა,რის მიმართაც იქმნება გაცვლითი კურსი, არ შეიძლება, რომ იყოს 'GEL'");
            await _repository.CreateExchangeRate(exchangeModel);
            await _repository.SaveChangesAsync();
            return _mapper.Map<ExchangeRateModel>(exchangeModel);
        }
    }
}
