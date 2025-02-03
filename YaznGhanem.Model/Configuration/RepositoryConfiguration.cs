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
    public class RepositoryConfiguration : IEntityTypeConfiguration<Repository>
    {
        public void Configure(EntityTypeBuilder<Repository> builder)
        {
            // Set the table name (optional, defaults to the class name)
            builder.ToTable("Repository");

            // Configure the primary key
            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            builder.Property(r => r.CategoryId)
                   .IsRequired();

            builder.Property(r => r.RepositoryMaterialsId)
                   .IsRequired();

            builder.Property(r => r.Name)
                   .IsRequired()
                   .HasMaxLength(100); // Example max length for name

            builder.Property(r => r.Sort)
                   .IsRequired();

            builder.Property(r => r.Amount_In)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // Define decimal precision for monetary values

            builder.Property(r => r.Amount_Out)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // Define decimal precision for monetary values

            builder.Property(r => r.Amount_Remender)
                   .IsRequired()
                   .HasColumnType("decimal(18,2)"); // Define decimal precision for monetary values

            // Configure the relationships
            builder.HasOne(r => r.Category)
                   .WithMany(c => c.Repositories)
                   .HasForeignKey(r => r.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); // Choose appropriate delete behavior

            builder.HasOne(r => r.RepositoryMaterials)
                   .WithMany(rm => rm.Repositories)
                   .HasForeignKey(r => r.RepositoryMaterialsId)
                   .OnDelete(DeleteBehavior.Restrict); // Choose appropriate delete behavior

         
        }
    }
}
