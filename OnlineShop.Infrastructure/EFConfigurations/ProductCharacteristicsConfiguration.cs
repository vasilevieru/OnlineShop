using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.EFConfigurations
{
    class ProductCharacteristicsConfiguration : EntityConfiguration<ProductCharacteristics>
    {
        public override void Configure(EntityTypeBuilder<ProductCharacteristics> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Key)
               .HasMaxLength(50)
               .IsRequired();

            builder.Property(x => x.Value)
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
