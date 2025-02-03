using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class BoFTotalConfiguration : IEntityTypeConfiguration<BoFTotal>
    {
        
        public void Configure(EntityTypeBuilder<BoFTotal> builder)
        {
            // Define the table name
            builder.ToTable("BoFTotal");

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);


            // Define properties
            // Configure the properties
            builder.Property(x => x.TotalIn)
                   .HasColumnName("TotalIn")
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(x => x.TotalOut)
                   .HasColumnName("TotalOut")
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(x => x.Current)
                   .HasColumnName("Current")
                   .HasColumnType("int")
                   .IsRequired();


        }
    }
}
