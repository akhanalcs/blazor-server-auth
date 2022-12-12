using HMT.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMT.Web.Server.Data
{
    public class DbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private const string AdminUserName = "admin@coolapp";
        private const string Admin2UserName = "MyADUsername"; // Valid Active Directory Username
        private const string HandyManUserName = "handyman@coolapp";

        private const string DefaultPassword = "Password123!";

        public DbInitializer(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
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
                await _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = RoleNames.Admin,
                    NormalizedName = RoleNames.Admin.ToUpper(),
                });

                // Create Admin user
                await _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = AdminUserName,
                    Email = AdminUserName
                }, DefaultPassword);

                adminUser = await _userManager.FindByNameAsync(AdminUserName);
                await _userManager.AddToRoleAsync(adminUser, RoleNames.Admin);
            }

            // Create Admin2 user
            var admin2User = await _userManager.FindByNameAsync(Admin2UserName);
            if (admin2User == null)
            {
                // Create Admin2 user
                await _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = Admin2UserName
                }, DefaultPassword);

                admin2User = await _userManager.FindByNameAsync(Admin2UserName);
                await _userManager.AddToRoleAsync(admin2User, RoleNames.Admin);
            }

            // Create Handyman user
            var handyManUser = await _userManager.FindByNameAsync(HandyManUserName);
            if (handyManUser == null)
            {
                // Create Handyman role
                await _roleManager.CreateAsync(new ApplicationRole
                {
                    Name = RoleNames.HandyMan,
                    NormalizedName = RoleNames.HandyMan.ToUpper(),
                });

                // Create Handyman user
                await _userManager.CreateAsync(new ApplicationUser
                {
                    UserName = HandyManUserName,
                    Email = HandyManUserName
                }, DefaultPassword);

                handyManUser = await _userManager.FindByNameAsync(HandyManUserName);
                await _userManager.AddToRoleAsync(handyManUser, RoleNames.HandyMan);
            }
        }
    }
}