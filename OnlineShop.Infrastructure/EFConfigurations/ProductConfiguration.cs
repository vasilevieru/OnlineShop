using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.EFConfigurations
{
    class ProductConfiguration : EntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.HasOne(x => x.Photo)
                .WithMany()
                .HasForeignKey(x => x.PhotoId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();


            builder.Property(x => x.Category)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.SubCategory)
                .HasMaxLength(100);

        }
    }
}
