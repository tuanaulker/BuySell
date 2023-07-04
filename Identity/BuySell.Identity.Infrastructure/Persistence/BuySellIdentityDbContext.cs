using BuySell.Identity.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Identity.Infrastructure.Persistence
{
    public class BuySellIdentityDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public BuySellIdentityDbContext(DbContextOptions options) : base(options) { }
    }
}
