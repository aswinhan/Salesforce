namespace SalesforceWeb.Dtos
{
    public class RolePermissionDto
    {
        public string RoleId { get; set; }
        public int ScreenId { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
