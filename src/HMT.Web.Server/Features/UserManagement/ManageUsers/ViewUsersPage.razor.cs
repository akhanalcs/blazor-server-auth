using Microsoft.AspNetCore.Components;
using HMT.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMT.Web.Server.Features.UserManagement.ManageUsers
{
    public partial class ViewUsersPage : ComponentBase
    {
        [Inject]
        public UserManager<ApplicationUser> UserManager { get; set; } = default!;
        public ICollection<ApplicationUser> Users { get; set; } = new List<ApplicationUser>();

        protected override async Task OnInitializedAsync()
        {
            Users = await UserManager.Users.ToListAsync();
        }
    }
}
