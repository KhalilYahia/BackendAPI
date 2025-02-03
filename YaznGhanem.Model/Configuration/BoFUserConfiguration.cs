using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YaznGhanem.Domain.Entities;

namespace YaznGhanem.Data.Configuration
{
    public class BoFUserConfiguration : IEntityTypeConfiguration<BoFUser>
    {
        public void Configure(EntityTypeBuilder<BoFUser> builder)
        {
            // Set the table name (optional, defaults to the class name)
            builder.ToTable("BoFUser");

            // Configure the primary key
            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            builder.Property(s => s.UserName)
                   .IsRequired()
                   .HasMaxLength(200); // Example max length for supplier name

            builder.Property(s => s.UserNameWithoutSpaces)
                  .IsRequired()
                  .HasMaxLength(200); // Example max length for supplier name

            // Configure the relationships
            builder.HasMany(s => s.BoFOperations)
                   .WithOne(fe => fe.BoFUser)
                   .HasForeignKey(fe => fe.BoFUserId)
                   .OnDelete(DeleteBehavior.Restrict); // Restrict delete behavior

           
        }
    }
}
