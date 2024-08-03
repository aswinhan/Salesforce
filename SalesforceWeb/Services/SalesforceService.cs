using SalesforceWeb.Services.IServices;
using System.Configuration;
using System.Net.Http.Headers;
using System.Text;

namespace SalesforceWeb.Services
{
    public class SalesforceService : ISalesforceService
    {
        private readonly HttpClient _httpClient;
        private readonly IOAuthService _oAuthService;
        private readonly IConfiguration _configuration;

        public SalesforceService(HttpClient httpClient, IOAuthService oAuthService, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _oAuthService = oAuthService;
            _configuration = configuration;
        }

        public async Task<string> PostToCompositeApiAsync(string jsonBody)
        {
            try
            {
                string token = await _oAuthService.GetBearerTokenAsync();
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var baseUrl = _configuration["Salesforce:BaseUrl"];
                var requestUri = $"{baseUrl}/services/data/v59.0/composite/";
                //var response = await _httpClient.GetAsync(requestUri);

                //string apiUrl = "https://mcal--mctraining.sandbox.my.salesforce.com/services/data/v59.0/composite/";

                //string apiUrl = "https://mcal.my.salesforce.com/services/data/v59.0/composite/";

                var content = new StringContent(jsonBody, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _httpClient.PostAsync(requestUri, content);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException($"Failed to send JSON body. Status code: {response.StatusCode}, Reason: {response.ReasonPhrase}, Response: {responseBody}");
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Error communicating with Salesforce API.", ex);
            }
        }
    }

}
