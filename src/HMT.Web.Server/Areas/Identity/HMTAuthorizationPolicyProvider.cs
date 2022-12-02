using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;

namespace HMT.Web.Server.Areas.Identity
{
    public class HMTAuthorizationPolicyProvider : DefaultAuthorizationPolicyProvider
    {
        private readonly AuthorizationOptions _options;

        public HMTAuthorizationPolicyProvider(IOptions<AuthorizationOptions> options) : base(options)
        {
            _options = options.Value;
        }

        public override async Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
        {
            var policy = await base.GetPolicyAsync(policyName);

            if(policy == null && PolicyNameHelper.IsValidPolicyName(policyName))
            {
                var permissions = PolicyNameHelper.GetPermissionsFrom(policyName);
                policy = new AuthorizationPolicyBuilder().AddRequirements(new HMTPermissionAuthorizationRequirement(permissions)).Build();
                _options.AddPolicy(policyName, policy);
            }

            return policy;
        }
    }
}
