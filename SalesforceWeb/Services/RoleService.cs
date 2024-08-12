using SalesforceWeb.Models;
using SalesforceWeb.Utilities;
using SalesforceWeb.Services.IServices;
using SalesforceWeb.Dtos;

namespace SalesforceWeb.Services
{
    public class RoleService : BaseService, IRoleService
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly string _salesforceUrl;

        public RoleService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            _salesforceUrl = configuration.GetValue<string>("ServiceUrls:SalesforceAPI");
        }

        public Task<T> GetAllRolesAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = $"{_salesforceUrl}/Role/",
                Token = token
            });
        }

        public Task<T> GetRolePermissionsAsync<T>(string roleId, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = $"{_salesforceUrl}/Role/{roleId}",
                Token = token
            });
        }

        public Task<T> UpdateRolePermissionsAsync<T>(List<RolePermissionDto> rolePermissionDtos, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Data = rolePermissionDtos, // Send the entire list of RolePermissionDto objects
                Url = $"{_salesforceUrl}/Role/",
                Token = token
            });
        }
    }
}
