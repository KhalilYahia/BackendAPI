using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class AdvertisementPropertiesConfiguration : IEntityTypeConfiguration<AdvertisementProperties>
    {
        
        public void Configure(EntityTypeBuilder<AdvertisementProperties> builder)
        {

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);

            builder.Property(m => m.AdvertismentId)
                .HasColumnName("AdvertismentId")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(m => m.PropertyId)
                .HasColumnName("PropertyId")
                .HasColumnType("int")
                .IsRequired();

            

            #region relationship config

            // one to many
            builder.HasOne<Advertisement>(s => s.Advertisement)
               .WithMany(s => s.AdvertisementProperties)
               .HasForeignKey(s => s.AdvertismentId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<Properties>(s => s.Property)
               .WithMany(s => s.AdvertisementProperties)
               .HasForeignKey(s => s.PropertyId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);


            #endregion



        }
    }
}
