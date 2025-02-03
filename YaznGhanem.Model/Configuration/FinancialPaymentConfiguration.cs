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
    public class FinancialPaymentConfiguration : IEntityTypeConfiguration<FinancialPayment>
    {
        public void Configure(EntityTypeBuilder<FinancialPayment> builder)
        {
            // Set the table name (optional, defaults to the class name)
            builder.ToTable("FinancialPayment");

            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            builder.Property(fp => fp.EntitlementId)
                   .IsRequired();

            builder.Property(fp => fp.AmountPayment)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // Define decimal precision for financial data

            builder.Property(fp => fp.PaymentDate)
                   .IsRequired();

            builder.Property(fp => fp.Notes)
                   .HasColumnType("nvarchar(max)");  // Example max length for notes

            // Configure the relationships
            builder.HasOne(fp => fp.FinancialEntitlement)
                   .WithMany(fe => fe.Paymenties) // Assuming FinancialEntitlement has a collection of FinancialPayments
                   .HasForeignKey(fp => fp.EntitlementId)
                   .OnDelete(DeleteBehavior.Restrict); // Choose appropriate delete behavior

            //builder.HasOne(fp => fp.Repository_In)
            //   .WithMany(ri => ri.Payments)
            //   .HasForeignKey(fp => fp.Repository_InId)
            //   .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
