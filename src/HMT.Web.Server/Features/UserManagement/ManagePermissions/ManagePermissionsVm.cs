using HMT.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Identity;

namespace HMT.Web.Server.Features.UserManagement.ManagePermissions
{
    public class ManagePermissionsVm
    {
        internal ManagePermissionsVm() { }

        public ManagePermissionsVm(List<HMTRole> roles)
        {
            Roles = roles;

            foreach (var permission in PermissionsProvider.GetAll())
            {
                if (permission == Permissions.None) continue;

                AvailablePermissions.Add(permission);
            }
        }

        public List<HMTRole> Roles { get; set; } = new();

        public List<Permissions> AvailablePermissions { get; set; } = new();
    }
}
