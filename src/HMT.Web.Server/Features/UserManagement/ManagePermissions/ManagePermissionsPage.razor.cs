using HMT.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMT.Web.Server.Features.UserManagement.ManagePermissions
{
    public partial class ManagePermissionsPage : ComponentBase
    {
        [Inject]
        public RoleManager<HMTRole> RoleManager { get; set; } = default!;
        private ManagePermissionsVm? _vm;

        protected override async Task OnInitializedAsync()
        {
            var roles = await RoleManager.Roles.ToListAsync();
            _vm = new ManagePermissionsVm(roles);
        }

        private async Task Set(HMTRole role, Permissions permission, bool granted)
        {
            if (granted)
            {
                await GrantPermissionToRoleAsync(role.Id, permission);
            }
            else
            {
                await RevokePermissionFromRoleAsync(role.Id, permission);
            }
        }

        private async Task GrantPermissionToRoleAsync(string roleId, Permissions permission)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            role.Permissions |= permission;
            await RoleManager.UpdateAsync(role);
        }

        private async Task RevokePermissionFromRoleAsync(string roleId, Permissions permission)
        {
            var role = await RoleManager.FindByIdAsync(roleId);
            role.Permissions ^= permission;
            await RoleManager.UpdateAsync(role);
        }
    }
}