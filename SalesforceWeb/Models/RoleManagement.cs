using SalesforceWeb.Dtos;

namespace SalesforceWeb.Models
{
    public class RoleManagement
    {
        public List<RoleDto> Roles { get; set; }
        public List<RolePermissionResponseDto> SelectedRolePermissions { get; set; }
        public string SelectedRoleId { get; set; }
        public string SelectedRoleName { get; set; }
    }
}
