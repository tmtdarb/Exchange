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
    public class ConversionConfigurations : IEntityTypeConfiguration<Conversion>
    {
        public void Configure(EntityTypeBuilder<Conversion> builder)
        {
            builder.ToTable("Conversions");
            builder.HasKey(a => a.ID);
            builder.HasOne(a => a.RecievedCurrency).WithMany(a=>a.RecievedCurrencies).HasForeignKey(a=>a.RecievedCurrencyID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(a=>a.SoldCurrency).WithMany(a=>a.SoldCurrencies).HasForeignKey(a=>a.SoldCurrencyID).OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(a=>a.SoldCurrency);
            builder.Property(a => a.AmountToBuy).HasColumnType("decimal(18,6)").IsRequired();
            builder.Property(a => a.AmountToSell).HasColumnType("decimal(18,6)").IsRequired();
            builder.Property(a => a.ConversionDate).IsRequired();
            builder.Property(a => a.Comment).HasMaxLength(120).IsRequired(false);
        }
    }
}
