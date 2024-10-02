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
        public Task<T> GetAsync<T>(string id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = salesforceUrl + "/Practitioner/" + id,
                Token = token
            });
        }
        public Task<T> EditAsync<T>(string id, string token)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticData.ApiType.GET,
                Url = salesforceUrl + "/GetPractitioner/" + id,
                Token = token
            });
        }
        public Task<T> UpdateAsync<T>(string id, string token, T practitioner)
        {
            return SendAsync<T>(new APIRequest()
            {
                ApiType = StaticData.ApiType.POST,
                Url = salesforceUrl + "/UpdatePractitioner/" + id,
                Data = practitioner,
                Token = token
            });
        }
    }
}
