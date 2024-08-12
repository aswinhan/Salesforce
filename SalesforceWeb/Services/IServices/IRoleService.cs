using SalesforceWeb.Dtos;

namespace SalesforceWeb.Services.IServices
{
    public interface IRoleService
    {
        Task<T> GetAllRolesAsync<T>(string token);
        Task<T> GetRolePermissionsAsync<T>(string roleId, string token);
        Task<T> UpdateRolePermissionsAsync<T>(List<RolePermissionDto> rolePermissionDto, string token);
    }
}
