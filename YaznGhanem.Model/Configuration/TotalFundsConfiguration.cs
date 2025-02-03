using YaznGhanem.Domain.Entities;
using YaznGhanem.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.ComponentModel.DataAnnotations.Schema;

namespace YaznGhanem.Model.Configuration
{
    public class TotalFundsConfiguration : IEntityTypeConfiguration<TotalFunds>
    {
        
        public void Configure(EntityTypeBuilder<TotalFunds> builder)
        {
            // Define the table name
            builder.ToTable("TotalFunds");

            builder.Property(x => x.Id)
             .HasColumnName("Id")
             .HasColumnType("int")
             .IsRequired()
             .UseIdentityColumn(seed:1, increment: 1);


            // Define properties
            builder.Property(t => t.TotalIn)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.TotalOut)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Profits)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.EarnedProfits)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.CurrentFund)
                .IsRequired()
                .HasColumnType("decimal(18,2)");


        }
    }
}
