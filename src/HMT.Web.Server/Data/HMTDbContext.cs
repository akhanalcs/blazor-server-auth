using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMT.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using HMT.Web.Server.Models;

namespace HMT.Web.Server.Data
{
    public class HMTDbContext : IdentityDbContext<HMTUser>
    {
        public HMTDbContext(DbContextOptions<HMTDbContext> options) : base(options) { }

        // Existing DbSet before I ran Identity Scaffolder
        public DbSet<RepairOrder> RepairOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
