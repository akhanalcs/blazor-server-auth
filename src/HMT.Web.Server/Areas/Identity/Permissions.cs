namespace HMT.Web.Server.Areas.Identity
{
    [Flags]
    //Since it has Flags attribute, it's Plural named
    public enum Permissions
    {
        None = 0,
        ViewUsers = 1,
        ManageUsers = 2,
        ViewRoles = 4,
        ManageRoles = 8,
        ViewPermissions = 16,
        ManagePermissions = 32,
        ViewCounter = 64,
        ViewWeatherForecast = 128,
        All = ~None
    }

    public static class PermissionsProvider
    {
        public static List<Permissions> GetAll()
        {
            return Enum.GetValues(typeof(Permissions))
                .OfType<Permissions>()
                .ToList();
        }
    }
}