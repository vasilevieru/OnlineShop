using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Persistence.Configurations
{
    class ProductConfiguration : EntityConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);
           
            builder.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();


            builder.Property(x => x.Category)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(x => x.SubCategory)
                .HasMaxLength(50);

        }
    }
}
