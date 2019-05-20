using Microsoft.EntityFrameworkCore;
using OnlineShop.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace OnlineShop.Application.Interfaces
{
    public interface IOnlineShopDbContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<Order> Orders { get; set; }
        DbSet<OrderLine> OrderLines { get; set; }
        DbSet<Image> Files { get; set; }
        DbSet<FileDescription> FileDescriptions { get; set; }
        DbSet<Product> Products { get; set; }
        DbSet<ProductCharacteristics> ProductCharacteristics { get; set; }
        DbSet<ShoppingCart> ShoppingCarts { get; set; }
        DbSet<ShoppingCartItem> ShoppingCartItems { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<RefreshToken> RefreshTokens { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
