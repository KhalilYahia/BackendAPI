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
    public class RepositoryMaterialsConfiguration : IEntityTypeConfiguration<RepositoryMaterials>
    {
        public void Configure(EntityTypeBuilder<RepositoryMaterials> builder)
        {
            // Set the table name (optional, defaults to the class name)
            builder.ToTable("RepositoryMaterials");

            // Configure the primary key
            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            builder.Property(rm => rm.CategoryId)
                   .IsRequired();

            builder.Property(rm => rm.Name)
                   .IsRequired()
                   .HasMaxLength(100); // Example max length

            builder.Property(rm => rm.DefaultPrice)
                   .HasColumnType("decimal(18,2)"); // Define decimal precision

            builder.Property(rm => rm.DefaultSoldPrice)
                   .HasColumnType("decimal(18,2)"); // Define decimal precision


            builder.Property(rm => rm.Sort)
                   .IsRequired();

            // Configure the relationships
            builder.HasOne(rm => rm.Category)
                   .WithMany(c => c.RepositoryMaterials)
                   .HasForeignKey(rm => rm.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); // Define delete behavior

            builder.HasMany(rm => rm.Repositories)
                   .WithOne(r => r.RepositoryMaterials)
                   .HasForeignKey(r => r.RepositoryMaterialsId)
                   .OnDelete(DeleteBehavior.Restrict); // Define delete behavior
        }
    }
}
