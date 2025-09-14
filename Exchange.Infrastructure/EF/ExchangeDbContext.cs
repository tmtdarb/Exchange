using Exchange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Infrastructure.EF
{
    public class ExchangeDbContext : DbContext
    {
        public ExchangeDbContext(DbContextOptions<ExchangeDbContext> options):base(options)
        {
            
        }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Conversion> Conversions { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new CurrencyConfigurations());
            modelBuilder.ApplyConfiguration(new ConversionConfigurations());
            modelBuilder.ApplyConfiguration(new ExchangeRateConfigurations());
        }
    }
}
