using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineShop.Domain.Entities;

namespace OnlineShop.Persistence.Configurations
{
    class RefreshTokenConfiguration : EntityConfiguration<RefreshToken>
    {
        public override void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            base.Configure(builder);

            builder
                .Property(x => x.Token)
                .IsRequired();

            builder
                .Property(x => x.Expires)
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithOne(x => x.RefreshToken)
                .HasForeignKey<RefreshToken>(x => x.UserId);
        }
    }
}
