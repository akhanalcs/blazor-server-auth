using HMT.Web.Server.Features.UserInformation;
using System.DirectoryServices;
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

        // Reference: https://stackoverflow.com/a/5310317/8644294
        public static UserInfo GetADUserInfo(string userName)
        {
            UserInfo userInfo = new ();
            userInfo.UserName = userName.ToUpper();

            // if you do repeated domain access, you might want to do this *once* outside this method, 
            // and pass it in as a second parameter!
            using PrincipalContext myDomain = new(ContextType.Domain);

            // find your user
            UserPrincipal user = UserPrincipal.FindByIdentity(myDomain, userName);

            // if found - grab its groups
            if (user != null)
            {
                PrincipalSearchResult<Principal> groups = user.GetAuthorizationGroups();

                // iterate over all groups
                foreach (Principal p in groups)
                {
                    // make sure to add only group principals
                    if (p is GroupPrincipal)
                    {
                        userInfo.GroupNames.Add(p.Name);
                    }
                }

                // Populate rest of the properties you need
                userInfo.FirstName = user.GivenName;
                userInfo.LastName = user.Surname;

                DirectoryEntry de = (user.GetUnderlyingObject() as DirectoryEntry);

                if (de != null)
                {
                    if (de.Properties.Contains("department"))
                    {
                        userInfo.DepartmentName = de.Properties["department"][0].ToString();
                    }
                }
            }

            return userInfo;
        }
    }
}
