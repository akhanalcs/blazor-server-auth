using HMT.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMT.Web.Server.Features.UserManagement.ManageUsers
{
    public partial class EditUserPage : ComponentBase
    {
        [Parameter]
        public string UserId { get; set; } = default!;

        [Inject]
        public UserManager<HMTUser> UserManager { get; set; } = default!;

        [Inject]
        public RoleManager<HMTRole> RoleManager { get; set; } = default!;

        [Inject]
        public NavigationManager Navigation { get; set; } = default!;

        public HMTUser User { get; set; } = default!;

        public ICollection<HMTRole> Roles { get; set; } = new List<HMTRole>();
        public ICollection<string> UserRoles { get; set; } = new List<string>();

        protected override async Task OnParametersSetAsync()
        {
            Roles = await RoleManager.Roles.ToListAsync();

            User = await UserManager.FindByIdAsync(UserId);

            UserRoles = await UserManager.GetRolesAsync(User);
        }

        public void ToggleSelectedRole(string roleName)
        {
            if (UserRoles.Contains(roleName))
            {
                UserRoles.Remove(roleName);
            }
            else
            {
                UserRoles.Add(roleName);
            }

            StateHasChanged();
        }

        public async Task UpdateUser()
        {
            var user = await UserManager.FindByIdAsync(User.Id);
            if (user == null) return;

            user.UserName = User.UserName;
            user.Email = User.Email;

            await UserManager.UpdateAsync(user);

            var currentRoles = await UserManager.GetRolesAsync(user);
            var addedRoles = UserRoles.Except(currentRoles);
            var removedRoles = currentRoles.Except(UserRoles);

            if (addedRoles.Any())
            {
                await UserManager.AddToRolesAsync(user, addedRoles);
            }

            if (removedRoles.Any())
            {
                await UserManager.RemoveFromRolesAsync(user, removedRoles);
            }

            Navigation.NavigateTo("/view/users");
        }
    }
}
