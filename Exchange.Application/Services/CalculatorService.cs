using Exchange.Application.DTO;
using Exchange.Application.Interfaces;
using Exchange.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.Services
{
    public class CalculatorService : ICalculatorService
    {
        private readonly IExchangeRatesRepository _ratesRepository;
        private readonly ICurrencyRepository _currencyRepository;
        public CalculatorService(IExchangeRatesRepository ratesRepository, ICurrencyRepository currencyRepository)
        {
            _ratesRepository = ratesRepository;
            _currencyRepository = currencyRepository;
        }
        public async Task<decimal> CalculateExchangeRate(string CurrencyCodeUserWantToGet, string CurrencyCodeUserWantToSell)
        {
            if (CurrencyCodeUserWantToGet == CurrencyCodeUserWantToSell)
                throw new InvalidOperationException("გასაყიდი და მისაღები ვალუტა არ უნდა ემთხვეოდეს ერთმანეთს");

            // მომხმარებლის მისაღები ვალუტა
            var recievedCurrency = await _currencyRepository.GetCurrencyByCode(CurrencyCodeUserWantToGet);
            if (recievedCurrency == null)
                throw new InvalidOperationException("მისაღები ვალუტა არ მოიძებნა");

            // მომხმარებლის გასაყიდი ვალუტა
            var soldCurrency = await _currencyRepository.GetCurrencyByCode(CurrencyCodeUserWantToSell);
            if (soldCurrency == null)
                throw new InvalidOperationException("გასაყიდი ვალუტა არ მოიძებნა");

            // ლარის ვალუტა
            var gelCurrency = await _currencyRepository.GetCurrencyByCode("gel");
            if (gelCurrency is null)
                throw new InvalidOperationException("ლარის ვალუტა ვერ მოიძებნა");

            // კურსი მისაღები ვალუტისთვის
            var exchangeRateForRecieved = await _ratesRepository.GetExchangeRateByCurrencyId(recievedCurrency.ID);
            if (exchangeRateForRecieved is null && gelCurrency.CurrencyCode != CurrencyCodeUserWantToGet)
                throw new InvalidOperationException("მისაღები ვალუტისთვის კურსი ვერ მოიძებნა");

            // კურსი გასაცემი ვალუტისთვის
            var exchangeRateForSold = await _ratesRepository.GetExchangeRateByCurrencyId(soldCurrency.ID);
            if (exchangeRateForSold is null && gelCurrency.CurrencyCode != CurrencyCodeUserWantToSell)
                throw new InvalidOperationException("გასაყიდი ვალუტისთვის კურსი ვერ მოიძებნა");

            decimal rateAmount;

            // მომხმარებლის მისაღები ვალუტა არის ლარი, გასაყიდი სხვა ნებისმიერი
            if (CurrencyCodeUserWantToGet == gelCurrency.CurrencyCode && CurrencyCodeUserWantToSell != gelCurrency.CurrencyCode)
                rateAmount = exchangeRateForSold.BuyRate;

            // მომხმარებლის გასაყიდი ვალუტა არის ლარი, მისაღები სხვა ნებისმიერი
            else if (CurrencyCodeUserWantToGet != gelCurrency.CurrencyCode && CurrencyCodeUserWantToSell == gelCurrency.CurrencyCode)
                rateAmount = 1m / exchangeRateForRecieved.SellRate;

            // მომხმარებლის არც გასაყიდი ვალუტაა ლარი, არც მისაღები
            else
                rateAmount = exchangeRateForSold.BuyRate / exchangeRateForRecieved.SellRate;
            return Math.Round(rateAmount, 4);

        }
        public async Task<CalculateAmountModel> CalculateAmountFromExchangeRate(string CurrencyCodeUserWantToGet, string CurrencyCodeUserWantToSell, decimal AmountToGet)
        {
            var exchangeRate = await CalculateExchangeRate(CurrencyCodeUserWantToGet, CurrencyCodeUserWantToSell);
            var result = new CalculateAmountModel();
            result.CurrencyCodeUserWantToGet = CurrencyCodeUserWantToGet;
            result.CurrencyCodeUserWantToSell = CurrencyCodeUserWantToSell;
            result.AmountToGet = AmountToGet;
            result.AmountToSell = Math.Round((AmountToGet / exchangeRate), 4);
            return result;
        }

    }
}
