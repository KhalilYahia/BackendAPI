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
    public class SupplierOfFarmsConfiguration : IEntityTypeConfiguration<SupplierOfFarms>
    {
        public void Configure(EntityTypeBuilder<SupplierOfFarms> builder)
        {
            // Set the table name (optional, defaults to the class name)
            builder.ToTable("SupplierOfFarms");

            // Configure the primary key
            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            builder.Property(s => s.Name)
                   .IsRequired()
                   .HasMaxLength(200); // Example max length for supplier name

            builder.Property(s => s.NameWithoutSpaces)
                  .IsRequired()
                  .HasMaxLength(200); // Example max length for supplier name

            // Configure the relationships
            builder.HasMany(s => s.FinancialEntitlements)
                   .WithOne(fe => fe.SupplierOfFarms)
                   .HasForeignKey(fe => fe.SupplierOfFarmId)
                   .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior

        }
    }
}
