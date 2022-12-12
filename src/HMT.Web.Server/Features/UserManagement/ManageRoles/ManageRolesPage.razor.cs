﻿using HMT.Web.Server.Areas.Identity;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HMT.Web.Server.Features.UserManagement.ManageRoles
{
    public partial class ManageRolesPage : ComponentBase
    {
        [Inject]
        public RoleManager<ApplicationRole> RoleManager { get; set; } = default!;
        public ICollection<ApplicationRole> Roles { get; set; } = new List<ApplicationRole>();
        private string newRoleName = string.Empty;
        private ApplicationRole? roleToEdit;

        protected override async Task OnInitializedAsync()
        {
            Roles = await RoleManager.Roles.ToListAsync();
        }

        private async Task AddRole()
        {
            if (!string.IsNullOrWhiteSpace(newRoleName))
            {
                var role = new ApplicationRole { Name = newRoleName };
                await RoleManager.CreateAsync(role);

                Roles.Add(role);
            }

            newRoleName = string.Empty;
        }

        private void EditRole(ApplicationRole role)
        {
            roleToEdit = role;
        }

        private void CancelEditRole()
        {
            roleToEdit = null;
        }

        private async Task UpdateRole()
        {
            if (roleToEdit == null) return;

            var role = await RoleManager.FindByIdAsync(roleToEdit.Id);
            role.Name = roleToEdit.Name;
            await RoleManager.UpdateAsync(role);

            roleToEdit = null;
        }

        private async Task DeleteRole(ApplicationRole roleToDelete)
        {
            var role = await RoleManager.FindByIdAsync(roleToDelete.Id);
            if (role == null) return;
            await RoleManager.DeleteAsync(role);
            Roles.Remove(role);
        }
    }
}
