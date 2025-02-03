using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class CityDescriptionConfiguration : IEntityTypeConfiguration<CityDescription>
    {
        
        public void Configure(EntityTypeBuilder<CityDescription> builder)
        {

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);

            builder.Property(m => m.CityId)
                .HasColumnName("CityId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(m => m.LanguageId)
                .HasColumnName("LanguageId")
                .HasColumnType("int")
                .IsRequired();

       

            builder.Property(m => m.CityName)
               .HasColumnName("CityName")
               .HasColumnType("nvarchar")
               .HasMaxLength(250)
               .IsRequired();

           

            #region relationship config

            // one to many
            builder.HasOne<Language>(s => s.Language)
               .WithMany(s => s.CityDescriptions)
               .HasForeignKey(s => s.LanguageId)
               .IsRequired()
               .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne<City>(s => s.City)
               .WithMany(s => s.CityDescription)
               .HasForeignKey(s => s.CityId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            

            #endregion



        }
    }
}
