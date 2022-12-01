namespace HMT.Web.Server.Areas.Identity
{
    public static class PolicyNameHelper
    {
        public const string Prefix = "Permissions";

        public static bool IsValidPolicyName(string? policyName)
        {
            return policyName != null && policyName.StartsWith(Prefix, StringComparison.OrdinalIgnoreCase);
        }

        public static string GeneratePolicyNameFor(Permissions permissions)
        {
            return $"{Prefix}{(int)permissions}";
        }

        public static Permissions GetPermissionsFrom(string policyName)
        {
            var permissionsValue = int.Parse(policyName[Prefix.Length..]);
            return (Permissions)permissionsValue;
        }

        /*
        Range example 1:
        string[] names =
        {
            "Archimedes", "Pythagoras", "Euclid"
        };
		
        foreach (var name in names[1..2])
        {
            Console.WriteLine(name); // Prints "Pythagoras"
        }

        Range example 2:
        var prefix = "Cool";
		var name = "Cool1";
        var myNum = name[prefix.Length..]!; // ! or not doesn't matter. It prints the same thing.
		Console.WriteLine(myNum); // Prints 1
        */
    }
}
