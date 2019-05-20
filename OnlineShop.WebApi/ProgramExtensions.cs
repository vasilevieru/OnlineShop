using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineShop.Application.Interfaces;
using OnlineShop.Persistence;
using OnlineShop.Persistence.Interfaces;
using System;

namespace OnlineShop.WebApi
{
    public static class ProgramExtensions
    {
        public static IWebHost MigrateDatabase(this IWebHost webHost, bool seed = false)
        {
            using (var scope = webHost.Services.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetService<IOnlineShopDbContext>();

                    var concreteContext = (OnlineShopDbContext)context;
                    concreteContext.Database.Migrate();
                    if (seed)
                        scope.ServiceProvider.GetRequiredService<ISeeder<OnlineShopDbContext>>().Seed(concreteContext);
                }
                catch (Exception ex)
                {
                    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }

            return webHost;
        }
    }
}
