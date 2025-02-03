using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class CategoryDescriptionConfiguration : IEntityTypeConfiguration<CategoryDescription>
    {
        
        public void Configure(EntityTypeBuilder<CategoryDescription> builder)
        {

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);

            builder.Property(m => m.CategoryId)
                .HasColumnName("CategoryId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(m => m.LanguageId)
                .HasColumnName("LanguageId")
                .HasColumnType("int")
                .IsRequired();

       

            builder.Property(m => m.Unit)
                .HasColumnName("Unit")
                .HasColumnType("nvarchar")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(m => m.CategoryName)
               .HasColumnName("CategoryName")
               .HasColumnType("nvarchar")
               .HasMaxLength(250)
               .IsRequired();

           

            #region relationship config

            // one to many
            builder.HasOne<Language>(s => s.Language)
               .WithMany(s => s.CategoryDescriptions)
               .HasForeignKey(s => s.LanguageId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<Category>(s => s.Category)
               .WithMany(s => s.CategoryDescriptions)
               .HasForeignKey(s => s.CategoryId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            

            #endregion



        }
    }
}
