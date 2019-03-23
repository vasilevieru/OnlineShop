using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.EFConfigurations
{
    class FileConfiguration : EntityConfiguration<File>
    {
        public override void Configure(EntityTypeBuilder<File> builder)
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
