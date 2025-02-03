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
    public class FinancialEntitlementConfiguration : IEntityTypeConfiguration<FinancialEntitlement>
    {
        public void Configure(EntityTypeBuilder<FinancialEntitlement> builder)
        {
            // Set the table name (optional, defaults to the class name)
            builder.ToTable("FinancialEntitlement");

            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            builder.Property(fe => fe.SupplierId);

            builder.Property(fe => fe.SupplierName)
                   .IsRequired()
                   .HasMaxLength(200); // Example max length for supplier name

            builder.Property(fe => fe.TotalAmount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // Define precision for monetary value

            builder.Property(fe => fe.TotalPayments)
                 .IsRequired()
                 .HasColumnType("decimal(18,2)"); // Define precision for monetary value

            builder.Property(fe => fe.Remainder)
                 .IsRequired()
                 .HasColumnType("decimal(18,2)"); // Define precision for monetary value

            builder.Property(fe => fe.Date)
                   .IsRequired();

            builder.Property(fe => fe.Notes)
                   .HasColumnType("nvarchar(max)"); // Allow unlimited text length

            // Configure the relationships
            builder.HasOne(fe => fe.Supplier)
                   .WithMany(s => s.FinancialEntitlements) // Assuming Supplier has a collection of FinancialEntitlements
                   .HasForeignKey(fe => fe.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict); // Choose appropriate delete behavior

            builder.HasOne(fe => fe.SupplierOfFarms)
                   .WithMany(s => s.FinancialEntitlements) // Assuming Supplier has a collection of FinancialEntitlements
                   .HasForeignKey(fe => fe.SupplierOfFarmId)
                   .OnDelete(DeleteBehavior.Restrict); // Choose appropriate delete behavior

            builder.HasMany(fe => fe.Paymenties)
                   .WithOne(fp => fp.FinancialEntitlement)
                   .HasForeignKey(fp => fp.EntitlementId)
                   .OnDelete(DeleteBehavior.Restrict); // Ensure payments are not deleted with the entitlement
        }
    }
}
