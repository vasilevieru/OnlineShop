using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Domain.Entities;
using OnlineShop.Persistence.Interfaces;
using System;
using System.Linq;
using System.Text;

namespace OnlineShop.Persistence.Data
{
    public class OnlineShopSeeder : ISeeder<OnlineShopDbContext>
    {
        public void Seed(OnlineShopDbContext dbContext)
        {
            if (!dbContext.Users.Any())
            {
                using (var hmac = new System.Security.Cryptography.HMACSHA512())
                {
                    var user = new User
                    {
                        FirstName = "Vasile",
                        LastName = "Vieru",
                        Phone = "+37379549996",
                        Role = "Admin",
                        Email = "vasilevieru@gmail.com",
                        PasswordSalt = hmac.Key,
                        PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes("adminvieru2019"))
                    };
                    dbContext.Users.Add(user);
                    dbContext.SaveChanges();
                };
            }

        }
    }
}
