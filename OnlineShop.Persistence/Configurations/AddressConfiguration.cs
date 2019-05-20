using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Persistence.Configurations
{
    class AddressConfiguration : EntityConfiguration<Address>
    {
        public override void Configure(EntityTypeBuilder<Address> builder)
        {
            base.Configure(builder);
            builder.Property(a => a.City)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.Country)
                .HasMaxLength(50)
                .IsRequired();

            builder.Property(a => a.Street)
               .HasMaxLength(100)
               .IsRequired();

            builder.Property(a => a.PostalCode)
               .HasMaxLength(20)
               .IsRequired();
        }
    }
}
