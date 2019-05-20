using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Persistence.Configurations
{
    class ImageConfiguration : EntityConfiguration<Image>
    {
        public override void Configure(EntityTypeBuilder<Image> builder)
        {
            base.Configure(builder);

            builder.Property(f => f.Name)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(f => f.MimeType)
                .HasMaxLength(20)
                .IsRequired();

            builder.Property(f => f.Path)
                .IsRequired();

            builder.HasIndex(f => f.Path)
                .IsUnique();
        }
    }
}
