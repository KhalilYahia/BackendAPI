using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class PropertiesConfiguration : IEntityTypeConfiguration<Properties>
    {
        
        public void Configure(EntityTypeBuilder<Properties> builder)
        {

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);

            builder.Property(m => m.Type)
                .HasColumnName("Type")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(m => m.TypeShow)
                .HasColumnName("TypeShow")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(m => m.TypeManagerShow)
               .HasColumnName("TypeManagerShow")
               .HasColumnType("int")
               .IsRequired();


            builder.Property(m => m.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(x => x.IsActive)
                 .HasColumnName("IsActive")
                 .HasColumnType("bit")
                 .IsRequired();

            #region relationship config

         
            // many to one
            builder.HasMany<AdvertisementProperties>(s => s.AdvertisementProperties)
              .WithOne(s => s.Property)
              .HasForeignKey(s => s.PropertyId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

           

            #endregion



        }
    }
}
