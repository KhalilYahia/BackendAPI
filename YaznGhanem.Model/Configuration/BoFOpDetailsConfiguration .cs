using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class BoFOpDetailsConfiguration : IEntityTypeConfiguration<BoFOpDetails>
    {
        
        public void Configure(EntityTypeBuilder<BoFOpDetails> builder)
        {
            // Define the table name
            builder.ToTable("BoFOpDetails");

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);


            // Define properties
            // Configure the properties
            builder.Property(x => x.Direction)
                  .HasColumnName("Direction")
                  .HasMaxLength(150) // Set a suitable max length for the string
                  .IsRequired();

            builder.Property(x => x.Count)
               .HasColumnName("Count")
               .HasColumnType("int")
               .IsRequired();

            builder.Property(x => x.ColorType)
                   .HasColumnName("ColorType")
                   .HasMaxLength(100) // Set a suitable max length for the color type
                   .IsRequired();

            // Foreign key for BoFOperationId
            builder.Property(x => x.BoFOperationId)
                   .HasColumnName("BoFOperationId")
                   .HasColumnType("int")
                   .IsRequired();

            // Configure the relationships
            builder.HasOne(x => x.BoFOperation)
                   .WithMany(op => op.BoFOpDetails)
                   .HasForeignKey(x => x.BoFOperationId)
                   .OnDelete(DeleteBehavior.Cascade); // Cascade delete behavior


        }
    }
}
