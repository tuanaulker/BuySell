using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Infrastructure.Persistence
{
    public class BusinessDbContextFactory : IDesignTimeDbContextFactory<BusinessDbContext>
    {
        //readonly IConfiguration _configuration;

        //public BusinessDbContextFactory(IConfiguration configuration)
        //{
        //    _configuration = configuration;
        //}

        public BusinessDbContext CreateDbContext(string[] args )
        {
            DbContextOptionsBuilder<BusinessDbContext> dbContextOptionsBuilder = new();
            //var connectionString = Configuration.GetConnectionString("BuySellConnection");
            dbContextOptionsBuilder.UseSqlServer("server=DESKTOP-OMG84RV\\SQLEXPRESS;database=BuySellBusinessDb;integrated security=true;TrustServerCertificate=True");
            return new(dbContextOptionsBuilder.Options);
        }
    }
}


/*
 readonly IConfiguration configuration;
        public BusinessDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BusinessDbContext>();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("BuySellConnection"));

            return new BusinessDbContext(optionsBuilder.Options);
        }
 

 BusinessDbContext IDesignTimeDbContextFactory<BusinessDbContext>.CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<BusinessDbContext>();
            var connectionString = configuration.GetConnectionString("BuySellConnection");

            builder.UseSqlServer(connectionString);

            return new BusinessDbContext(builder.Options);
        }
 */