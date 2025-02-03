using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class FavoriteConfiguration : IEntityTypeConfiguration<Favorite>
    {
        
        public void Configure(EntityTypeBuilder<Favorite> builder)
        {

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);


            builder.Property(x => x.AddingDate)
                .HasColumnName("AddingDate")
                .HasColumnType("datetime2")
                .IsRequired()
                ;

            builder.Property(m => m.AdvertismentId)
               .HasColumnName("AdvertismentId")
               .HasColumnType("int")
               .IsRequired();

            builder.Property(m => m.UserId)
              .HasColumnName("UserId")
              .HasColumnType("nvarchar")
              .HasMaxLength(450)
              .IsRequired();

           

            #region relationship config

            // one to many
            builder.HasOne<CustomUser>(s => s.User)
               .WithMany(s => s.Favorites)
               .HasForeignKey(s => s.UserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<Advertisement>(s => s.Advertisement)
               .WithMany(s => s.Favorites)
               .HasForeignKey(s => s.AdvertismentId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);


            #endregion



        }
    }
}
