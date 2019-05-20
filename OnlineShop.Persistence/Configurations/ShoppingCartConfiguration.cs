using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Persistence.Configurations
{
    class ShoppingCartConfiguration : EntityConfiguration<ShoppingCart>
    {
        public override void Configure(EntityTypeBuilder<ShoppingCart> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.CreatedAt)
               .IsRequired();

            builder
                 .HasOne(x => x.User)
                 .WithOne(x => x.ShoppingCart)
                 .HasForeignKey<ShoppingCart>(x => x.UserId);
        }
    }
}
