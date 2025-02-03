using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Data.Configuration
{
    public class FuelConfiguration : IEntityTypeConfiguration<Fuel>
    {
        public void Configure(EntityTypeBuilder<Fuel> builder)
        {
            // Table name
            builder.ToTable("Fuel");

            // Primary key
            builder.Property(x => x.Id)
              .HasColumnName("Id")
              .HasColumnType("int")
              .IsRequired()
              .UseIdentityColumn(seed: 1, increment: 1);

            // Properties
            builder.Property(f => f.SourceName)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.Property(f => f.Type)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(f => f.Amount)
                .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(f => f.PriceOfOne)
                .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(f => f.TotalPrice)
                .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(f => f.Date)
                   .IsRequired();

            builder.Property(f => f.Notes)
                   .HasMaxLength(2000);

            // Relationships
            builder.HasOne(f => f.Source)
                   .WithMany(f=>f.Fuels)
                   .HasForeignKey(f => f.SourceId);

            builder.HasOne(f => f.FinancialEntitlement)
                   .WithMany(f => f.Fuels)
                   .HasForeignKey(f => f.EntitlementId);
        }
    }
}
