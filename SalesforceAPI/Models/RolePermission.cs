using Microsoft.AspNetCore.Identity;

namespace SalesforceAPI.Models
{
    public class Screen
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }

    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<RolePermission> RolePermissions { get; set; }
    }

    public class RolePermission
    {
        public int RolePermissionId { get; set; }

        public string RoleId { get; set; }
        public IdentityRole Role { get; set; }

        public int ScreenId { get; set; }
        public Screen Screen { get; set; }

        public int PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
