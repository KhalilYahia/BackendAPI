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
    public class RefrigeratorDetailsConfiguration : IEntityTypeConfiguration<RefrigeratorDetails>
    {
        public void Configure(EntityTypeBuilder<RefrigeratorDetails> builder)
        {
            // Table name and schema
            builder.ToTable("RefrigeratorDetails");

            // Primary Key
            builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .IsRequired()
            .UseIdentityColumn(seed: 1, increment: 1);

            builder.Property(e => e.CountOfBoxes)
               .HasColumnType("decimal(18,2)");

            builder.Property(e => e.BalanceCardWeight)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.EmptyBoxesWeight)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.WeightAfterDiscount_2Percent)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.SalesPriceOfUnit)
                .HasColumnType("decimal(18,2)");

            builder.Property(e => e.SalesPriceOfAll)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(e => e.Refrigerator)
                .WithMany(r => r.RefrigeratorDetails)
                .HasForeignKey(e => e.RefrigeratorId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.RepositoryMaterial)
                .WithMany(r => r.RefrigeratorDetails)
                .HasForeignKey(e => e.RepositoryMaterialId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
