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
    public class RepositoryInOutConfiguration : IEntityTypeConfiguration<Repository_InOut>
    {
        public void Configure(EntityTypeBuilder<Repository_InOut> builder)
        {
            // Set the table name (optional, defaults to the class name)
            builder.ToTable("Repository_InOut");

            // Configure the primary key
            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            builder.Property(ri => ri.RepositoryMaterialId)
                   .IsRequired();

            builder.Property(ri => ri.Name)
                   .IsRequired()
                   .HasMaxLength(100); // Example max length for name

            builder.Property(ri => ri.Amount)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // Define decimal precision for amounts

            builder.Property(ri => ri.BuyPriceOfUnit)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(ri => ri.BuyPriceOfAll)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(ri => ri.SoldPriceOfUnit)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(ri => ri.SoldPriceOfAll)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)");

            builder.Property(ri => ri.SupplierName)
                   .HasMaxLength(200); // Example max length for supplier name

            builder.Property(ri => ri.SupplierId)
                   .IsRequired();

            builder.Property(ri => ri.Direction)
                   .IsRequired();

            builder.Property(ri => ri.Date)
                   .IsRequired();

            builder.Property(ri => ri.Notes)
                   .HasColumnType("nvarchar(max)"); // Allow unlimited text length

            // Configure the relationships
            builder.HasOne(ri => ri.RepositoryMaterial)
                   .WithMany(r => r.Repository_InOuts)
                   .HasForeignKey(ri => ri.RepositoryMaterialId)
                   .OnDelete(DeleteBehavior.Restrict); // Choose appropriate delete behavior

            builder.HasOne(ri => ri.Supplier)
                   .WithMany(s => s.Repository_InOuts) // Assuming Supplier has a collection of Repository_InOuts
                   .HasForeignKey(ri => ri.SupplierId)
                   .OnDelete(DeleteBehavior.Restrict); // Choose appropriate delete behavior
         
            builder.HasOne(r => r.FinancialEntitlement)
                .WithMany(s => s.Repository_Ins)
                .HasForeignKey(r => r.EntitlementId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
