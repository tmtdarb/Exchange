using Exchange.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Application.Interfaces
{
    public interface ICalculatorService
    {
        Task<decimal> CalculateExchangeRate(string CurrencyCodeUserWantToGet, string CurrencyCodeUserWantToSell);
        Task<CalculateAmountModel> CalculateAmountFromExchangeRate(string CurrencyCodeUserWantToGet, string CurrencyCodeUserWantToSell, decimal AmountToGet);
    }
}
