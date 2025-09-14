using Exchange.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exchange.Infrastructure.EF
{
    public class ExchangeRateConfigurations : IEntityTypeConfiguration<ExchangeRate>
    {
        public void Configure(EntityTypeBuilder<ExchangeRate> builder)
        {
            builder.ToTable("ExchangeRates");
            builder.HasKey(a => a.ID);
            builder.HasOne(a => a.Currency).WithMany(a=>a.ExchangeRates).HasForeignKey(a=>a.CurrencyID).OnDelete(DeleteBehavior.Cascade);
            builder.Property(a => a.BuyRate).HasColumnType("decimal(18,6)").IsRequired();
            builder.Property(a => a.SellRate).HasColumnType("decimal(18,6)").IsRequired();
            builder.Property(a => a.CreatedAt).IsRequired();
        }
    }
}
