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

namespace Exchange.Application.CQRS.Conversions.Commands
{
    public record CreateConversionCommand(CreateConversionModel model) : IRequest<ConversionModel>;
    public class CreateConversionCommandHandler : IRequestHandler<CreateConversionCommand, ConversionModel>
    {
        private readonly IConversionsRepository _conversionRepository;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IExchangeRatesRepository _exchangeRatesRepository;
        private readonly IMapper _mapper;
        public CreateConversionCommandHandler(IConversionsRepository conversionRepository, IMapper mapper, ICurrencyRepository currencyRepository, IExchangeRatesRepository exchangeRatesRepository)
        {
            _conversionRepository = conversionRepository;
            _mapper = mapper;
            _currencyRepository = currencyRepository;
            _exchangeRatesRepository = exchangeRatesRepository;
        }
        public async Task<ConversionModel> Handle(CreateConversionCommand request, CancellationToken cancellationToken)
        {
            var recievedCurrency = await _currencyRepository.GetCurrencyByCode(request.model.RecievedCurrencyCode);
            var soldCurrency = await _currencyRepository.GetCurrencyByCode(request.model.SoldCurrencyCode);
            if (recievedCurrency is null)
                throw new InvalidOperationException("მისაღები ვალუტა არ მოიძებნა");
            if (soldCurrency is null)
                throw new InvalidOperationException("გასაცემი ვალუტა არ მოიძებნა");

            var exchRateRecievedCurrency = await _exchangeRatesRepository.GetExchangeRateByCurrencyId(recievedCurrency.ID);
            var exchRateSoldCurrency = await _exchangeRatesRepository.GetExchangeRateByCurrencyId(soldCurrency.ID);
            if (exchRateRecievedCurrency is null && recievedCurrency.CurrencyCode != "GEL")
                throw new InvalidOperationException("მისაღები ვალუტის კურსი არ მოიძებნა");
            if (exchRateSoldCurrency is null && soldCurrency.CurrencyCode != "GEL")
                throw new InvalidOperationException("გასაცემი ვალუტის კურსი არ მოიძებნა");

            decimal sellAmountInGel;
            if (soldCurrency.CurrencyCode == "GEL")
                sellAmountInGel = request.model.AmountToSell;
            else if (recievedCurrency.CurrencyCode == "GEL")
                sellAmountInGel = request.model.AmountToSell * exchRateSoldCurrency.BuyRate;
            else
                sellAmountInGel = request.model.AmountToSell * exchRateRecievedCurrency.BuyRate;

            if(sellAmountInGel > 3000 && string.IsNullOrEmpty(request.model.Comment))
                throw new InvalidOperationException("თანხა აღემატება 3000 ლარს, აუცილებელია კომენტარის მითითება");

            var model = _mapper.Map<Conversion>(request.model);
            model.SoldCurrencyID =soldCurrency.ID;
            model.RecievedCurrencyID = recievedCurrency.ID;
            model.ConversionDate = DateTimeOffset.Now;
            await _conversionRepository.AddConversion(model);
            await _conversionRepository.SaveChanges();
            return _mapper.Map<ConversionModel>(model);
        }
    }
}
