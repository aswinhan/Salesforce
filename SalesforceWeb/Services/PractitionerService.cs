using SalesforceWeb.Models;
using SalesforceWeb.Services.IServices;
using SalesforceWeb.Utilities;

namespace SalesforceWeb.Services
{
    public class PractitionerService : BaseService, IPractitionerService
    {
        private readonly IHttpClientFactory _clientFactory;
        private string salesforceUrl;

        public PractitionerService(IHttpClientFactory clientFactory, IConfiguration configuration) : base(clientFactory)
        {
            _clientFactory = clientFactory;
            salesforceUrl = configuration.GetValue<string>("ServiceUrls:SalesforceAPI");

        }
        public Task<T> GetAsync<T>(string id)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = salesforceUrl + "/Practitioner/" + id
            });
        }
    }
}
