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
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            // Set the table name (optional, defaults to the class name)
            builder.ToTable("Supplier");

            // Configure the primary key
            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            builder.Property(s => s.SupplierName)
                   .IsRequired()
                   .HasMaxLength(200); // Example max length for supplier name

            // Configure the relationships
            builder.HasMany(s => s.FinancialEntitlements)
                   .WithOne(fe => fe.Supplier)
                   .HasForeignKey(fe => fe.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior

          

            builder.HasMany(s => s.Repository_InOuts)
                   .WithOne(ri => ri.Supplier)
                   .HasForeignKey(ri => ri.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior
        }
    }
}
