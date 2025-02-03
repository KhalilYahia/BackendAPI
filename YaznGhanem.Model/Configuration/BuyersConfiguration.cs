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
    public class BuyersConfiguration : IEntityTypeConfiguration<Buyers>
    {
        public void Configure(EntityTypeBuilder<Buyers> builder)
        {
            // Table name and schema
            builder.ToTable("Buyers");

            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            // Property configurations
            builder.Property(b => b.Name)
                .IsRequired()
                .HasMaxLength(200); // Set a reasonable length for the name

            builder.Property(b => b.BuyerNameWithoutSpaces)
                .IsRequired()
                .HasMaxLength(200); // Assuming it's also required and with the same length

            // Relationship configurations
            builder.HasMany(b => b.Refrigerators)
                .WithOne(r => r.Buyer)
                .HasForeignKey(r => r.BuyerId);

            builder.HasMany(b => b.ExternalEnvoices)
                .WithOne(e => e.Buyer)
                .HasForeignKey(e => e.BuyerId);

            builder.HasMany(b => b.CoolingRooms)
                .WithOne(cr => cr.Client)
                .HasForeignKey(cr => cr.ClientId);

            builder.HasMany(b => b.OtherSales)
                .WithOne(os => os.Buyer)
                .HasForeignKey(os => os.BuyerId);

          
        }
    }
}
