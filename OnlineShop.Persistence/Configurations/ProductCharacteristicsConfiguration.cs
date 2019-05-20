using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Persistence.Configurations
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
