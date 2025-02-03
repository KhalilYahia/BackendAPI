using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        
        public void Configure(EntityTypeBuilder<Advertisement> builder)
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

            builder.Property(m => m.Class)
                .HasColumnName("Class")
                .HasColumnType("int")
                .IsRequired();

            builder.Property(m => m.Status)
               .HasColumnName("Status")
               .HasColumnType("int")
               .IsRequired();


            builder.Property(m => m.Name)
                .HasColumnName("Name")
                .HasColumnType("nvarchar")
                .HasMaxLength(500)
                .IsRequired();

            builder.Property(m => m.Description)
               .HasColumnName("Description")
               .HasColumnType("nvarchar")
               .HasMaxLength(1000)
               .IsRequired();

            builder.Property(m => m.Address)
               .HasColumnName("Address")
               .HasColumnType("nvarchar")
               .HasMaxLength(500)
               .IsRequired(false);

            builder.Property(m => m.Longitude)
               .HasColumnName("Longitude")
               .HasColumnType("nvarchar")
               .HasMaxLength(250)
               .IsRequired(false);

            builder.Property(m => m.Latitude)
               .HasColumnName("Latitude")
               .HasColumnType("nvarchar")
               .HasMaxLength(250)
               .IsRequired(false);

           

            builder.Property(m => m.PhoneNumber_Call)
              .HasColumnName("PhoneNumber_Call")
              .HasColumnType("nvarchar")
              .HasMaxLength(15)
              .IsRequired(false);

            builder.Property(m => m.WhatsApp)
              .HasColumnName("WhatsApp")
              .HasColumnType("nvarchar")
              .HasMaxLength(15)
              .IsRequired(false);

            builder.Property(m => m.Telegram)
              .HasColumnName("Telegram")
              .HasColumnType("nvarchar")
              .HasMaxLength(100);

            builder.Property(m => m.Facebook)
              .HasColumnName("Facebook")
              .HasColumnType("nvarchar")
              .HasMaxLength(250)
              .IsRequired(false);

            builder.Property(x => x.Price)
              .HasColumnName("Price")
              .HasColumnType("money")
              .IsRequired(false);


            builder.Property(x => x.AddingDate)
                .HasColumnName("AddingDate")
                .HasColumnType("datetime2")
                .IsRequired()
                ;

            builder.Property(m => m.AdvertiserUserId)
              .HasColumnName("AdvertiserUserId")
              .HasColumnType("nvarchar")
              .HasMaxLength(450)
              .IsRequired();

            builder.Property(m => m.CityId)
               .HasColumnName("CityId")
               .HasColumnType("int")
               .IsRequired();

            builder.Property(m => m.CategoryId)
               .HasColumnName("CategoryId")
               .HasColumnType("int")
               .IsRequired();

            #region relationship config

            // one to many
            builder.HasOne<CustomUser>(s => s.Advertiser)
               .WithMany(s => s.Advertisements)
               .HasForeignKey(s => s.AdvertiserUserId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<City>(s => s.City)
               .WithMany(s => s.Advertisements)
               .HasForeignKey(s => s.CityId)
               .IsRequired()
               .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasOne<Category>(s => s.Category)
              .WithMany(s => s.Advertisements)
              .HasForeignKey(s => s.CategoryId)
              .IsRequired()
              .OnDelete(DeleteBehavior.ClientCascade);

            // many to one
            builder.HasMany<ImageOfAdvertisment>(s => s.Images)
              .WithOne(s => s.Advertisement)
              .HasForeignKey(s => s.AdvertismentId)
              .IsRequired()
              .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<AdvertisementProperties>(s => s.AdvertisementProperties)
             .WithOne(s => s.Advertisement)
             .HasForeignKey(s => s.AdvertismentId)
             .IsRequired()
             .OnDelete(DeleteBehavior.ClientCascade);

            builder.HasMany<Favorite>(s => s.Favorites)
             .WithOne(s => s.Advertisement)
             .HasForeignKey(s => s.AdvertismentId)
             .IsRequired()
             .OnDelete(DeleteBehavior.ClientCascade);

            // one to one
            builder.HasOne<AdvertismentSearch>(s => s.AdvertismentSearch)
             .WithOne(s => s.Advertisement)
             .HasForeignKey<AdvertismentSearch>(s=>s.AdvertisementId)
             .IsRequired()
             .OnDelete(DeleteBehavior.ClientCascade);

            #endregion



        }
    }
}
