namespace HMT.Web.Server.Features.UserInformation.ADUserInfo
{
    public class UserInfo
    {
        private string? departmentName;

        public string UserName { get; set; } = default!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<string> GroupNames { get; set; } = new();
        public string CommaSeparatedGroupNames
        {
            get => string.Join(",", GroupNames);
        }

        // Noticed that this property comes with lots of whitespaces, so decided to trim them. For eg: "Dept1         "
        public string? DepartmentName
        {
            get => departmentName;
            set => departmentName = value?.Trim();
        }
    }
}
