using SalesforceWeb.Models;
using SalesforceWeb.Utilities;
using SalesforceWeb.Services.IServices;

namespace SalesforceWeb.Services
{
    public class AuditService : BaseService, IAuditService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string salesforceUrl;

        public AuditService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            salesforceUrl = configuration.GetValue<string>("ServiceUrls:SalesforceAPI");
        }

        public Task<T> GetAsync<T>(string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = salesforceUrl + "/Audit",
                Token = token
            });
        }
    }
}
