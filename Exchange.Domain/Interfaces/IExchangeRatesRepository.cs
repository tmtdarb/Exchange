using Exchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Interfaces
{
    public interface IExchangeRatesRepository
    {
        Task<List<ExchangeRate>> GetAllExchangeRates();
        Task<List<ExchangeRate>> GetAllLatestExchangeRates();
        Task CreateExchangeRate(ExchangeRate exchangeRate);
        Task<ExchangeRate> GetExchangeRateById(int id);
        Task<ExchangeRate> GetExchangeRateByCurrencyId(int id);
        Task DeleteExchangeRateById(int id);
        Task UpdateExchangeRate(ExchangeRate exchangeRate);
        Task SaveChangesAsync();
    }
}
