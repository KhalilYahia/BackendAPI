using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class CityConfiguration : IEntityTypeConfiguration<City>
    {
        
        public void Configure(EntityTypeBuilder<City> builder)
        {

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);

            builder.Property(m => m.Sort)
                .HasColumnName("Sort")
                .HasColumnType("int")
                .IsRequired();

           

            #region relationship config

           

            // many to one
            builder.HasMany<CityDescription>(s => s.CityDescription)
              .WithOne(s => s.City)
              .HasForeignKey(s => s.CityId)
              .IsRequired()
              .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany<Advertisement>(s => s.Advertisements)
             .WithOne(s => s.City)
             .HasForeignKey(s => s.CityId)
             .IsRequired()
             .OnDelete(DeleteBehavior.Restrict);

          

            #endregion



        }
    }
}
