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
            var exchangeRates = await _db.ExchangeRates.Include(a=>a.Currency).ToListAsync();
            return exchangeRates;
        }
    }
}
