using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace HMT.Web.Server.Areas.Identity
{
    public class HMTUserClaimsPrincipalFactory : UserClaimsPrincipalFactory<HMTUser, HMTRole>
    {
        public HMTUserClaimsPrincipalFactory(UserManager<HMTUser> userManager, RoleManager<HMTRole> roleManager, IOptions<IdentityOptions> optionsAccessor)
            : base(userManager, roleManager, optionsAccessor)
        { }

        protected override async Task<ClaimsIdentity> GenerateClaimsAsync(HMTUser user)
        {
            var claimsIdentity = await base.GenerateClaimsAsync(user);
            var userRoleNames = await UserManager.GetRolesAsync(user) ?? Array.Empty<string>();

            var userRoles = await RoleManager.Roles.Where(r => userRoleNames.Contains(r.Name)).ToListAsync();
            var userPermissions = Permissions.None;

            foreach (var role in userRoles)
            {
                userPermissions |= role.Permissions;
            }

            var permissionsValue = (int)userPermissions;

            claimsIdentity.AddClaim(new Claim(nameof(Permissions), permissionsValue.ToString()));

            return claimsIdentity;
        }
    }
}
