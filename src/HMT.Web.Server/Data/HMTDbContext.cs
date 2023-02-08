﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HMT.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace HMT.Web.Server.Data
{
    public class HMTDbContext : IdentityDbContext<HMTUser, HMTRole, string>
    {
        public HMTDbContext(DbContextOptions<HMTDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
