using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Core.Domain;
using OnlineShop.Core.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineShop.Infrastructure.EFConfigurations
{
    class OrderConfiguration : EntityConfiguration<Order>
    {
        public override void Configure(EntityTypeBuilder<Order> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Number)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(o => o.Description)
                .HasMaxLength(1000);

            builder.Property(o => o.OrderDate)
                .IsRequired();

            builder.Property(x => x.TotalAmount)
                .IsRequired();

        }
    }
}
