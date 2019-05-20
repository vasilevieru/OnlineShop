using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Persistence.Configurations
{
    class FileDescriptionConfiguration : EntityConfiguration<FileDescription>
    {
        public override void Configure(EntityTypeBuilder<FileDescription> builder)
        {
            base.Configure(builder);

            builder.Property(f => f.FileName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(f => f.OriginalName)
                .HasMaxLength(250)
                .IsRequired();

            builder.Property(f => f.FileType)
                .HasMaxLength(11)
                .IsRequired();
        }
    }
}
