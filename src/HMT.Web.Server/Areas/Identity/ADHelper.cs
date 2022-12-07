using System.DirectoryServices.AccountManagement;

namespace HMT.Web.Server.Areas.Identity
{
    public static class ADHelper
    {
        public static bool ADLogin(string userName, string password)
        {
            using PrincipalContext principalContext = new(ContextType.Domain);
            bool isValidLogin = principalContext.ValidateCredentials(userName.ToUpper(), password);

            return isValidLogin;
        }
    }
}
