using AutoMapper;
using Exchange.Application.DTO;
using Exchange.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.CQRS.ExchangeRates.Commands
{
    public record UpdateExchangeRateCommand(int id, CreateExchangeRateModel model) : IRequest<ExchangeRateModel>;
    public class EditExchangeRateCommandHandler : IRequestHandler<UpdateExchangeRateCommand, ExchangeRateModel>
    {
        private readonly IExchangeRatesRepository _exchangeRatesRepository;
        private readonly IMapper _mapper;
        public EditExchangeRateCommandHandler(IExchangeRatesRepository exchangeRatesRepository, IMapper mapper)
        {
            _exchangeRatesRepository = exchangeRatesRepository;
            _mapper = mapper;
        }
        public async Task<ExchangeRateModel> Handle(UpdateExchangeRateCommand request, CancellationToken cancellationToken)
        {
            var er = await _exchangeRatesRepository.GetExchangeRateById(request.id);
            if (er is null)
                throw new InvalidOperationException("ვალუტის კურსი ამ აიდით არ მოიძებნა");
            er.SellRate = request.model.SellRate;
            er.BuyRate = request.model.BuyRate;
            er.CurrencyID = request.model.CurrencyID;
            er.CreatedAt = request.model.CreatedAt;
            await _exchangeRatesRepository.UpdateExchangeRate(er);
            await _exchangeRatesRepository.SaveChangesAsync();
            return _mapper.Map<ExchangeRateModel>(er);
        }
    }
}
