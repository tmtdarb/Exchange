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
    public class ConversionRepository : IConversionsRepository
    {
        private readonly ExchangeDbContext _db;
        public ConversionRepository(ExchangeDbContext db)
        {
            _db = db;
        }
        public async Task AddConversion(Conversion model)
        {
            await _db.Conversions.AddAsync(model);
        }
        public async Task<List<Conversion>> GetAllConversions()
        {
            var result = await _db.Conversions.Include(a => a.SoldCurrency).Include(a => a.RecievedCurrency).ToListAsync();
            return result;
        }
        public async Task<List<Conversion>> GetAllConversionsByDate(DateTime from, DateTime to)
        {
            var result = await _db.Conversions.Where(a=>a.ConversionDate>=from && a.ConversionDate<=to).Include(a => a.SoldCurrency).Include(a => a.RecievedCurrency).ToListAsync();
            return result;
        }
        public async Task<Conversion> GetConversionByID(int id)
        {
            var result = await _db.Conversions.Include(a=>a.SoldCurrency).Include(a=>a.RecievedCurrency).FirstOrDefaultAsync(a=>a.ID == id);
            return result;
        }
        public async Task SaveChanges()
        {
            await _db.SaveChangesAsync();
        }
    }
}
