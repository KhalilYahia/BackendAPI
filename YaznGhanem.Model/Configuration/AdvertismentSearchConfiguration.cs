using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class AdvertismentSearchConfiguration : IEntityTypeConfiguration<AdvertismentSearch>
    {
        
        public void Configure(EntityTypeBuilder<AdvertismentSearch> builder)
        {

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);

            builder.Property(m => m.AdvertisementId)
                .HasColumnName("AdvertisementId")
                .HasColumnType("int")
                .IsRequired();

          

            builder.Property(m => m.InternalData)
                .HasColumnName("InternalData")
                .HasColumnType("nvarchar")
                .HasMaxLength(4000)
                .IsRequired();

           

            #region relationship config

            // one to one
            builder.HasOne<Advertisement>(s => s.Advertisement)
             .WithOne(s => s.AdvertismentSearch)
             .HasForeignKey<AdvertismentSearch>(s=>s.AdvertisementId)
             .IsRequired()
             .OnDelete(DeleteBehavior.ClientCascade);

            #endregion



        }
    }
}
