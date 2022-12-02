using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace HMT.Web.Server.Areas.Identity
{
    public class HMTPermissionAuthorizationHandler : AuthorizationHandler<HMTPermissionAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HMTPermissionAuthorizationRequirement requirement)
        {
            var permissionClaim = context.User.FindFirst(c => c.Type == nameof(Permissions));

            if (permissionClaim == null)
            {
                return Task.CompletedTask;
            }

            if(!int.TryParse(permissionClaim.Value, out int permissionClaimValue))
            {
                return Task.CompletedTask;
            }

            var userPermissions = (Permissions)permissionClaimValue;
            if ((userPermissions & requirement.Permissions) != 0)
            {
                context.Succeed(requirement);
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
