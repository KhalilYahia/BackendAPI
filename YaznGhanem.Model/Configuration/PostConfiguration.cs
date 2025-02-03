using firstProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace firstProject.Model.Configuration
{
    public class PostConfiguration: IEntityTypeConfiguration<Post>
    {
        
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.Property(m => m.PostId).IsRequired();
            builder.Property(m => m.Content)
                .IsRequired()
                .HasColumnName("Content")
                .HasColumnType("nvarchar")
                .HasMaxLength(1000);

        }
    }
}
