using BuySell.Business.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Business.Infrastructure.Persistence
{
    public class BusinessDbContext : DbContext
    {
        public BusinessDbContext(DbContextOptions options) : base(options) { }
        public DbSet<UserInfo> UserInfos { get; set; }
        public DbSet<Categories> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        //Table Per Hierarchy(TPH)
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }

        public DbSet<Orders> Orders { get; set; }

    }
}
