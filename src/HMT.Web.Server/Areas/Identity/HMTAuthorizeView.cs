using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace HMT.Web.Server.Areas.Identity
{
    public class HMTAuthorizeView : AuthorizeView
    {
        [Parameter] // Because we'll use it like this: <HMTAuthorizeView Permissions="@(Permissions.SomePermission></HMTAuthorizeView>
        public Permissions Permissions
        {
            get
            {
                return string.IsNullOrEmpty(Policy) ? Permissions.None : PolicyNameHelper.GetPermissionsFrom(Policy);
            }
            set
            {
                Policy = PolicyNameHelper.GeneratePolicyNameFor(value);
            }
        }
    }
}