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
    public class CurrencyConfigurations : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.ToTable("Currencies");
            builder.HasKey(a => a.ID);
            builder.Property(a=>a.CurrencyCode).IsRequired().HasMaxLength(5);
            builder.Property(a=>a.CurrencyName).IsRequired().HasMaxLength(30);
            builder.Property(a=>a.CurrencyNameEn).IsRequired().HasMaxLength(30);
            builder.Property(a => a.OrderNumber).IsRequired();
            builder.HasIndex(a => a.CurrencyCode).IsUnique();
            builder.HasIndex(a => a.OrderNumber);
            builder.Property(a=>a.IsActive).HasDefaultValue(true);
        }
    }
}
