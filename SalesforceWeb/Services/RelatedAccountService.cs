using SalesforceWeb.Services.IServices;
using System.Net.Http.Headers;

namespace SalesforceWeb.Services
{
    
    public class RelatedAccountService : IRelatedAccountService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public RelatedAccountService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetRelatedAccount(string token, string id)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var apiUrl = _configuration["Salesforce:BaseUrl"];
                var requestUri = $"{apiUrl}/services/apexrest/api/Account/{id}";
                var response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    return responseBody;
                }
                else
                {
                    throw new HttpRequestException($"Failed to fetch data from Salesforce API: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error: " + ex.Message);
            }
        }
    }
}
