using BuySell.Business.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Infrastructure
{
    public static class InfrastructureBusinessServiceRegistration
    {
        public static void AddBusinessPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BusinessDbContext>(options => options.UseSqlServer
                (configuration.GetConnectionString("BuySellConnection")));
        }

        public static void Migration(IServiceScope scope)
        {
            var dataContext = scope.ServiceProvider.GetRequiredService<BusinessDbContext>();
            dataContext.Database.Migrate();
        }
    }
}
