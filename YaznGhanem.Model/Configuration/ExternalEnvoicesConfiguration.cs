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
    public class ExternalEnvoicesConfiguration : IEntityTypeConfiguration<ExternalEnvoices>
    {
        public void Configure(EntityTypeBuilder<ExternalEnvoices> builder)
        {
            // Table name and schema
            builder.ToTable("ExternalEnvoices");

            // Primary Key
            builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .IsRequired()
            .UseIdentityColumn(seed: 1, increment: 1);

            // Property configurations
            builder.Property(e => e.MaterialName)
                .IsRequired()
                .HasMaxLength(200); // Set a reasonable length for the material name

            builder.Property(e => e.TotalBoxes)
                .IsRequired();

            builder.Property(e => e.Weight)
                 .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.CodeNumber)
                .HasMaxLength(100); // Assuming it has a maximum length

            builder.Property(e => e.SalesPriceOfUnit)
                 .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.SalesPriceOfAll)
                 .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(e => e.BuyerName)
                .HasMaxLength(200); // Assuming it has a maximum length

            builder.Property(e => e.Date)
                .IsRequired();

            builder.Property(e => e.Notes)
                .HasMaxLength(3000); // Assuming notes have a maximum length

            // Relationship configurations
            builder.HasOne(e => e.Buyer)
                .WithMany(b => b.ExternalEnvoices)
                .HasForeignKey(e => e.BuyerId);

            builder.HasOne(e => e.RepositoryMaterial)
                .WithMany(rm => rm.ExternalEnvoices)
                .HasForeignKey(e => e.RepositoryMaterialId);

           
        }
    }
}
