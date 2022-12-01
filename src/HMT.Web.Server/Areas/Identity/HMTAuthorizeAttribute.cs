using Microsoft.AspNetCore.Authorization;

namespace HMT.Web.Server.Areas.Identity
{
    public class HMTAuthorizeAttribute : AuthorizeAttribute
    {
        public HMTAuthorizeAttribute() { }
        public HMTAuthorizeAttribute(string policy) : base(policy) { }
        public HMTAuthorizeAttribute(Permissions permission)
        {
            Permissions = permission;
        }

        public Permissions Permissions 
        {
            get
            {
                return string.IsNullOrEmpty(Policy) ? Permissions.None : PolicyNameHelper.GetPermissionsFrom(Policy);
            }
            set
            {
                Policy = value == Permissions.None ? string.Empty : PolicyNameHelper.GeneratePolicyNameFor(value);
            }
        }
    }
}
