using BuySell.Identity.Domain.Entities;
using BuySell.Identity.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace BuySell.Identity.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BuySellIdentityDbContext>(options => options.UseSqlServer
                (configuration.GetConnectionString("BuySellConnection")));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 3;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }
            ).AddEntityFrameworkStores<BuySellIdentityDbContext>();
        }

        public static void Migration(IServiceScope scope)
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<BuySellIdentityDbContext>();
            dataContext.Database.Migrate();
        }
    }
}
