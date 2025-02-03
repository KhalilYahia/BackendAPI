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
    public class CoolingRoomsConfiguration : IEntityTypeConfiguration<CoolingRooms>
    {
        public void Configure(EntityTypeBuilder<CoolingRooms> builder)
        {
            // Table name and schema
            builder.ToTable("CoolingRooms");

            // Primary Key
            builder.Property(x => x.Id)
           .HasColumnName("Id")
           .HasColumnType("int")
           .IsRequired()
           .UseIdentityColumn(seed: 1, increment: 1);

            // Property configurations
            builder.Property(cr => cr.Room)
                .IsRequired()
                .HasMaxLength(200); // Set a reasonable length for the room name

            builder.Property(cr => cr.MaterialName)
                .IsRequired()
                .HasMaxLength(200); // Set a reasonable length for the material name

            builder.Property(cr => cr.TotalBoxes)
                .IsRequired();

            builder.Property(cr => cr.Weight)
                 .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(cr => cr.CodeNumber)
                .HasMaxLength(100); // Assuming it has a maximum length

            builder.Property(cr => cr.CostOfUnit)
                 .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(cr => cr.CostOfAll)
                 .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(cr => cr.ClientName)
                .HasMaxLength(200); // Assuming it has a maximum length

            builder.Property(cr => cr.Date)
                .IsRequired();

            builder.Property(cr => cr.Notes)
                .HasMaxLength(3000); // Assuming notes have a maximum length

            // Relationship configurations
            builder.HasOne(cr => cr.Client)
                .WithMany(b => b.CoolingRooms)
                .HasForeignKey(cr => cr.ClientId);

            builder.HasOne(cr => cr.RepositoryMaterial)
                .WithMany(rm => rm.CoolingRooms)
                .HasForeignKey(cr => cr.RepositoryMaterialId);

        }
    }
}
