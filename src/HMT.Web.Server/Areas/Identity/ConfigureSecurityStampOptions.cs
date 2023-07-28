using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace HMT.Web.Server.Areas.Identity
{
	public class ConfigureSecurityStampOptions : IConfigureOptions<SecurityStampValidatorOptions>
	{
		public void Configure(SecurityStampValidatorOptions options)
		{
			options.ValidationInterval = TimeSpan.FromMinutes(1);

			// When refreshing the principal, ensure custom claims that
			// might have been set with an external identity continue
			// to flow through to this new one.
			options.OnRefreshingPrincipal = refreshingPrincipal =>
			{
				ClaimsIdentity? newIdentity = refreshingPrincipal.NewPrincipal?.Identities.First();
				ClaimsIdentity? currentIdentity = refreshingPrincipal.CurrentPrincipal?.Identities.First();

				if (currentIdentity is not null && newIdentity is not null)
				{
					// Since this is refreshing an existing principal, we want to merge all claims.
					// Only work with claims in current identity that are not already present in the new identity with the same Type and Value.
					var currentClaimsNotInNewIdentity = currentIdentity.Claims.Where(c => !newIdentity.HasClaim(c.Type, c.Value));

					foreach (Claim claim in currentClaimsNotInNewIdentity)
					{
						newIdentity.AddClaim(claim);
					}
				}

				return Task.CompletedTask;
			};
		}
	}
}
