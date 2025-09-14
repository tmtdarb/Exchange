using Exchange.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Domain.Interfaces
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetAllActiveCurrencies();
        Task<Currency> GetCurrencyById(int id);
        Task CreateCurrency(Currency currency);
        Task UpdateCurrency(Currency currency);
        Task SaveChangesAsync();
    }
}
