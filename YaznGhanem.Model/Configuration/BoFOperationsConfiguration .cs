using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class BoFOperationsConfiguration : IEntityTypeConfiguration<BoFOperations>
    {
        
        public void Configure(EntityTypeBuilder<BoFOperations> builder)
        {
            // Define the table name
            builder.ToTable("BoFOperations");

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);


            // Define properties
            // Configure the properties
            builder.Property(x => x.Count)
               .HasColumnName("Count")
               .HasColumnType("int")
               .IsRequired();

            //builder.Property(x => x.Direction)
            //       .HasColumnName("Direction")
            //       .HasMaxLength(150) // Set a suitable max length for the string
            //       .IsRequired();

            builder.Property(x => x.BoFUserId)
                   .HasColumnName("BoFUserId")
                   .HasColumnType("int")
                   .IsRequired();

            builder.Property(x => x.BoFUserName)
                   .HasColumnName("BoFUserName")
                   .HasMaxLength(200) // Set a suitable max length for the username
                   .IsRequired();

            builder.Property(x => x.Date)
                   .HasColumnName("Date")
                   .HasColumnType("datetime")
                   .IsRequired();

            builder.Property(x => x.Notes)
                   .HasColumnName("Notes")
                   .HasMaxLength(1000); // Set a max length for the notes

            // Configure the relationships
            builder.HasOne(x => x.BoFUser)
                   .WithMany(u => u.BoFOperations)
                   .HasForeignKey(x => x.BoFUserId)
                   .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior

            builder.HasMany(x => x.BoFOpDetails)
                   .WithOne(op => op.BoFOperation)
                   .HasForeignKey(op => op.BoFOperationId)
                   .OnDelete(DeleteBehavior.Cascade); // Cascade delete behavior for details


        }
    }
}
