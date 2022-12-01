using Microsoft.AspNetCore.Identity;

namespace HMT.Web.Server.Areas.Identity
{
    public class HMTRole : IdentityRole
    {
        // This will add a column called Permissions (int, not null) in dbo.AspNetRoles table
        public Permissions Permissions { get; set; }

        public bool Has(Permissions permission)
        {
            return Permissions.HasFlag(permission);
        }
    }
}