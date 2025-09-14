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
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly ExchangeDbContext _db;
        public CurrencyRepository(ExchangeDbContext db)
        {
            _db = db;
        }
        public async Task<List<Currency>> GetAllActiveCurrencies()
        {
            var activeCurrencies = await _db.Currencies.Where(a => a.IsActive).ToListAsync();
            return activeCurrencies;
        }
        public async Task<Currency> GetCurrencyById(int id)
        {
            var currency = await _db.Currencies.FirstOrDefaultAsync(a => a.ID == id);
            return currency;
        }
        public async Task CreateCurrency(Currency currency)
        {
            _db.Currencies.AddAsync(currency);
        }
        public async Task UpdateCurrency(Currency currency)
        {
            _db.Currencies.Update(currency);
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
