using Microsoft.AspNetCore.Authorization;

namespace HMT.Web.Server.Areas.Identity
{
    public class HMTPermissionAuthorizationRequirement : IAuthorizationRequirement
    {
        public HMTPermissionAuthorizationRequirement(Permissions permissions)
        {
            Permissions = permissions;
        }

        public Permissions Permissions { get; }
    }
}
