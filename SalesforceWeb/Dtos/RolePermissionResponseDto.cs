namespace SalesforceWeb.Dtos
{
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
}
