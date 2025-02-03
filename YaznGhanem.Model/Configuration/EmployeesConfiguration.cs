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

    public class EmployeesConfiguration : IEntityTypeConfiguration<Employees>
    {
        public void Configure(EntityTypeBuilder<Employees> builder)
        {
            // Table name
            builder.ToTable("Employees");

            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            // Properties
            builder.Property(e => e.workshopName)
                   .IsRequired()
                   .HasMaxLength(200);

           

            builder.Property(e => e.NormHWageM)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.NormHWageG)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.AdditionalWorkingHourWage)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.TotalWage)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.TotalRewards)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.TotalDiscount)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.TotalWageAfterDiscount)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.Payments)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.Remainder)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.Date)
                   .IsRequired();

            builder.Property(e => e.Notes)
                   .HasMaxLength(3000);

            // Relationships
            builder.HasMany(e => e.DailyChekEmployees)
                   .WithOne(e=>e.Employee)
                   .HasForeignKey(d => d.EmployeeId);
        }
    }
}
