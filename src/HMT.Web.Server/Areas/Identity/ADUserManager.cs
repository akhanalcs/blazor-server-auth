using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace HMT.Web.Server.Areas.Identity
{
    public class ADUserManager<TUser> : UserManager<TUser> where TUser : IdentityUser
    {
        public ADUserManager(IUserStore<TUser> store, IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<TUser> passwordHasher, IEnumerable<IUserValidator<TUser>> userValidators,
            IEnumerable<IPasswordValidator<TUser>> passwordValidators, ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<TUser>> logger)
            : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer,
                  errors, services, logger)
        {
        }

        public override Task<bool> CheckPasswordAsync(TUser user, string password)
        {
            var adLoginResult = ADHelper.ADLogin(user.UserName, password);
            return Task.FromResult(adLoginResult);
        }
    }
}
