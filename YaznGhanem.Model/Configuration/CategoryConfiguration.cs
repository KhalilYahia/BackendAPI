using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        
        public void Configure(EntityTypeBuilder<Category> builder)
        {

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);

            builder.Property(c => c.CategoryName)
                     .IsRequired()
                     .HasMaxLength(100); // Example max length for category name

            builder.Property(c => c.Unit)
                   .IsRequired()
                   .HasMaxLength(50); // Example max length for unit

            // Configure the relationships
            builder.HasMany(c => c.RepositoryMaterials)
                   .WithOne(rm => rm.Category)
                   .HasForeignKey(rm => rm.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); // Choose appropriate delete behavior

            builder.HasMany(c => c.Repositories)
                   .WithOne(r => r.Category)
                   .HasForeignKey(r => r.CategoryId)
                   .OnDelete(DeleteBehavior.Restrict); // Choose appropriate delete behavior




        }
    }
}
