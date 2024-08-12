namespace SalesforceAPI.Dtos
{
    public class RoleDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class RolePermissionResponseDto
    {
        public int ScreenId { get; set; }
        public string ScreenName { get; set; }
        public List<PermissionCheckboxDto> Permissions { get; set; }
    }

    public class PermissionCheckboxDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }


    public class RolePermissionDto
    {
        public string RoleId { get; set; }
        public int ScreenId { get; set; }
        public List<int> PermissionIds { get; set; }
    }
}
