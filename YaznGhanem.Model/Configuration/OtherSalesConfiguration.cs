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
    public class OtherSalesConfiguration : IEntityTypeConfiguration<OtherSales>
    {
        public void Configure(EntityTypeBuilder<OtherSales> builder)
        {
            // Table name and schema
            builder.ToTable("OtherSales");

            // Primary Key
            builder.Property(x => x.Id)
            .HasColumnName("Id")
            .HasColumnType("int")
            .IsRequired()
            .UseIdentityColumn(seed: 1, increment: 1);

            // Property configurations
            builder.Property(os => os.SalesPriceOfAll)
                 .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(os => os.BuyerName)
                .HasMaxLength(200); // Assuming it has a maximum length

            builder.Property(os => os.Date)
                .IsRequired();
           

            builder.Property(os => os.Notes)
                .HasMaxLength(3000); // Assuming notes have a maximum length

            // Relationship configurations
            builder.HasOne(os => os.Buyer)
                .WithMany(b => b.OtherSales)
                .HasForeignKey(os => os.BuyerId);

            builder.HasOne(r => r.RepositoryMaterial)
                .WithMany(rm => rm.OtherSales)
                .HasForeignKey(r => r.RepositoryMaterialId);
            // Additional configurations if necessary
        }
    }
}
