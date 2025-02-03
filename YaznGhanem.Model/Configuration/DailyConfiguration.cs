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
    public class DailyConfiguration : IEntityTypeConfiguration<Daily>
    {
        public void Configure(EntityTypeBuilder<Daily> builder)
        {
            // Table name
            builder.ToTable("Dailies");

            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            // Properties
            builder.Property(d => d.MaterialName)
                   .IsRequired()
                   .HasMaxLength(100);

            builder.Property(d => d.TotalBoxes)
                   .IsRequired();

            builder.Property(d => d.BalanceCardWeight)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.EmptyBoxesWeight)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.WeightAfterDiscount_2Percent)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.CodeNumber)
                   .HasMaxLength(50);

            builder.Property(d => d.BuyPriceOfUnit)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.BuyPriceOfAll)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.CuttingCostOfAll)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.CuttingCostOfUnit)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.WaxingCostOfUnit)
                .HasColumnType("decimal(18,2)")
                  .IsRequired();

            builder.Property(d => d.WaxingCostOfAll)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();


            builder.Property(d => d.FarmerName)
                   .HasMaxLength(100);

            builder.Property(d => d.Supplier)
                   .HasMaxLength(100);

            builder.Property(d => d.Date)
                   .IsRequired();


            builder.Property(d => d.Notes)
                   .HasMaxLength(500);

            // Relationships
            builder.HasOne(d => d.Farmer)
                   .WithMany()
                   .HasForeignKey(d => d.FarmerId);

            builder.HasOne(d => d.FinancialEntitlement)
                   .WithMany(d=>d.Dailies)
                   .HasForeignKey(d => d.EntitlementId);

            builder.HasOne(d => d.SupplierOfFarmsFinancialEntitlement)
                   .WithMany(d => d.SupplierOfFarmsDailies)
                   .HasForeignKey(d => d.SupplierOfFarmsEntitlementId);

            builder.HasOne(d => d.RepositoryMaterial)
                   .WithMany(d => d.Dailies)
                   .HasForeignKey(d => d.RepositoryMaterialId);

            builder.HasOne(d => d.SupplierOfFarms)
                   .WithMany(q => q.Dailies)
                   .HasForeignKey(d => d.SupplierOfFarmsId);

            builder.HasOne(d => d.WaxingFactory_As_dealer)
                  .WithMany(q => q.DailiesForWaxingFactoryDealer)
                  .HasForeignKey(d => d.WaxingFactory_As_dealerId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(d => d.WaxingFactoryFinancialEntitlement)
                   .WithMany(d => d.WaxingFactoryDailies)
                   .HasForeignKey(d => d.WaxingFactoryEntitlementId);
        }
    }
}
