using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class ImageOfAdvertismentConfiguration : IEntityTypeConfiguration<ImageOfAdvertisment>
    {
        
        public void Configure(EntityTypeBuilder<ImageOfAdvertisment> builder)
        {

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);

          

            builder.Property(m => m.Path)
                .HasColumnName("Path")
                .HasColumnType("nvarchar")
                .HasMaxLength(1000)
                .IsRequired();

            builder.Property(x => x.IsPrimary)
                .HasColumnName("IsPrimary")
                .HasColumnType("bit")
                .IsRequired();

            builder.Property(m => m.AdvertismentId)
                .HasColumnName("AdvertismentId")
                .HasColumnType("int")
                .IsRequired();

            #region relationship config

            // one to many
            builder.HasOne<Advertisement>(s => s.Advertisement)
               .WithMany(s => s.Images)
               .HasForeignKey(s => s.AdvertismentId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

         

            #endregion



        }
    }
}
