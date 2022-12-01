using Microsoft.AspNetCore.Components;
using HMT.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMT.Web.Server.Features.UserManagement.ManageUsers
{
    public partial class ViewUsersPage : ComponentBase
    {
        [Inject]
        public UserManager<HMTUser> UserManager { get; set; } = default!;
        public ICollection<HMTUser> Users { get; set; } = new List<HMTUser>();

        protected override async Task OnInitializedAsync()
        {
            Users = await UserManager.Users.ToListAsync();
        }
    }
}
