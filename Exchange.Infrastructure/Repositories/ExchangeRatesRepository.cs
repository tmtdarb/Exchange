using Exchange.Domain.Entities;
using Exchange.Domain.Interfaces;
using Exchange.Infrastructure.EF;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Infrastructure.Repositories
{
    public class ExchangeRatesRepository : IExchangeRatesRepository
    {
        private readonly ExchangeDbContext _db;
        public ExchangeRatesRepository(ExchangeDbContext db)
        {
            _db = db;
        }
        public async Task<List<ExchangeRate>> GetAllExchangeRates()
        {
            var exchangeRates = await _db.ExchangeRates.Where(a => a.Currency.IsActive).Include(a => a.Currency).ToListAsync();
            return exchangeRates;
        }
        public async Task<List<ExchangeRate>> GetAllLatestExchangeRates()
        {
            var exchangeRates = await _db.ExchangeRates.Where(a => a.Currency.IsActive).Include(a => a.Currency).GroupBy(a=>a.CurrencyID).Select(a=>a.OrderByDescending(a=>a.CreatedAt).First()).ToListAsync();
            return exchangeRates;
        }
        public async Task CreateExchangeRate(ExchangeRate exchangeRate)
        {
            await _db.ExchangeRates.AddAsync(exchangeRate);
        }
        public async Task<ExchangeRate> GetExchangeRateById(int id)
        {
            var result = await _db.ExchangeRates.Where(a => a.Currency.IsActive).Include(a=>a.Currency).FirstOrDefaultAsync(a => a.ID == id);
            return result;
        }
        public async Task<ExchangeRate> GetExchangeRateByCurrencyId(int id)
        {
            var result = await _db.ExchangeRates.Where(a => a.CurrencyID == id).OrderByDescending(a=>a.CreatedAt).FirstOrDefaultAsync();
            return result;
        }
        public async Task DeleteExchangeRateById(int id)
        {
            var er = await _db.ExchangeRates.FirstOrDefaultAsync(a => a.ID == id);
            if (er != null)
                _db.ExchangeRates.Remove(er);
        }
        public async Task UpdateExchangeRate(ExchangeRate exchangeRate)
        {
            _db.ExchangeRates.Update(exchangeRate);

        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
