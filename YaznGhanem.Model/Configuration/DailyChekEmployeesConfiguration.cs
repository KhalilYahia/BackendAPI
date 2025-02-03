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
    public class DailyChekEmployeesConfiguration : IEntityTypeConfiguration<DailyChekEmployees>
    {
        public void Configure(EntityTypeBuilder<DailyChekEmployees> builder)
        {
            // Table name
            builder.ToTable("DailyChekEmployees");

            // Primary key
            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed: 1, increment: 1);

            // Properties
            builder.Property(e => e.GirlsCount)
                  .IsRequired();

            builder.Property(e => e.MenCount)
                   .IsRequired();

            builder.Property(e => e.NormHWageM)
                 .HasColumnType("decimal(18,2)")
                  .IsRequired();

            builder.Property(e => e.NormHWageG)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(e => e.AdditionalWorkingHourWage)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.NormJobHCount)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.AddJobHCount)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.TotalWage)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.Reward)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.Discount)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.ResultWage)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.PaidWage)
                 .HasColumnType("decimal(18,2)")
                   .IsRequired();

            builder.Property(d => d.Date)
                   .IsRequired();

            builder.Property(d => d.Notes)
                   .HasMaxLength(3000);

            // Relationships
            builder.HasOne(d => d.Employee)
                   .WithMany(e => e.DailyChekEmployees)
                   .HasForeignKey(d => d.EmployeeId);
        }
    }
}
