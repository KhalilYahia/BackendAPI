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
    public class RefrigeratorConfiguration : IEntityTypeConfiguration<Refrigerator>
    {
        public void Configure(EntityTypeBuilder<Refrigerator> builder)
        {
            // Table name and schema
            builder.ToTable("Refrigerator");

            // Primary Key
            builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .IsRequired()
            .UseIdentityColumn(seed: 1, increment: 1);

         

            builder.Property(r => r.TotalBoxes)
                .HasColumnType("decimal(18,2)")
                .IsRequired();
          

            builder.Property(r => r.Date)
                .IsRequired();

            builder.Property(r => r.Notes)
                .HasMaxLength(5000); // Assuming notes have a maximum length

            builder.Property(e => e.BuyerName)
                  .HasMaxLength(100);

            builder.Property(e => e.TotalBalanceCardWeight)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.TotalEmptyBoxesWeight)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.TotalWeightAfterDiscount_2Percent)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.TotalSalesPriceOfAll)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(e => e.Buyer)
                .WithMany(b => b.Refrigerators)
                .HasForeignKey(e => e.BuyerId)
                .OnDelete(DeleteBehavior.Restrict);


            builder.HasMany(e => e.RefrigeratorDetails)
                .WithOne(d => d.Refrigerator)
                .HasForeignKey(d => d.RefrigeratorId);


        }
    }
}
