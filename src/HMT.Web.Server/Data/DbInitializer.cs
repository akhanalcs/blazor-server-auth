using HMT.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMT.Web.Server.Data
{
    public class DbInitializer
    {
        private readonly HMTDbContext _context;
        private readonly UserManager<HMTUser> _userManager;
        private readonly RoleManager<HMTRole> _roleManager;

        private const string AdminRole = "Administrators";
        private const string AdminUserName = "admin@coolapp";
        private const string HandyManRole = "HandyMan";
        private const string HandyManUserName = "handyman@coolapp";

        private const string DefaultPassword = "Password123!";

        public DbInitializer(HMTDbContext context, UserManager<HMTUser> userManager, RoleManager<HMTRole> roleManager)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task InitializeAsync()
        {
            if (_context.Database.GetPendingMigrations().Any())
            {
                await _context.Database.MigrateAsync();
            }

            // Create Admin user
            var adminUser = await _userManager.FindByNameAsync(AdminUserName);
            if (adminUser == null)
            {
                //Create Admin role
                await _roleManager.CreateAsync(new HMTRole
                {
                    Name = AdminRole,
                    NormalizedName = AdminRole.ToUpper(),
                    Permissions = Permissions.All
                });

                // Create Admin user
                await _userManager.CreateAsync(new HMTUser
                {
                    UserName = AdminUserName,
                    Email = AdminUserName
                }, DefaultPassword);

                adminUser = await _userManager.FindByNameAsync(AdminUserName);
                await _userManager.AddToRoleAsync(adminUser, AdminRole);
            }

            // Create Handyman user
            var handyManUser = await _userManager.FindByNameAsync(HandyManUserName);
            if (handyManUser == null)
            {
                // Create Handyman role
                await _roleManager.CreateAsync(new HMTRole
                {
                    Name = HandyManRole,
                    NormalizedName = HandyManRole.ToUpper(),
                    Permissions = Permissions.ViewCounter | Permissions.ViewWeatherForecast
                });

                // Create Handyman user
                await _userManager.CreateAsync(new HMTUser
                {
                    UserName = HandyManUserName,
                    Email = HandyManUserName
                }, DefaultPassword);

                handyManUser = await _userManager.FindByNameAsync(HandyManUserName);
                await _userManager.AddToRoleAsync(handyManUser, HandyManRole);
            }
        }
    }
}
