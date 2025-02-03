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
    public class CarsConfiguration : IEntityTypeConfiguration<Cars>
    {
        public void Configure(EntityTypeBuilder<Cars> builder)
        {
            // Table name
            builder.ToTable("Cars");

            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            // Properties
            builder.Property(c => c.DriverName)
                   .IsRequired()
                   .HasMaxLength(300);

            builder.Property(c => c.LoadsPerDay)
                   .IsRequired();

            builder.Property(c => c.PriceOfOne)
                .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(c => c.TotalPrice)
                .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(c => c.Date)
                   .IsRequired();

            builder.Property(c => c.Notes)
                   .HasMaxLength(500);

            // Relationships
            builder.HasOne(c => c.Driver)
                   .WithMany(c=>c.Cars)
                   .HasForeignKey(c => c.DriverId);

            builder.HasOne(c => c.FinancialEntitlement)
                   .WithMany(c => c.Cars)
                   .HasForeignKey(c => c.EntitlementId);
        }
    }
}
