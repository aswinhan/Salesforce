//using System.Text;
//using System.Text.Json;

using Newtonsoft.Json;
using SalesforceWeb.Services.IServices;
using System.Text;

namespace SalesforceWeb.Services
{
    public class OAuthService: IOAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public OAuthService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _configuration = configuration;
        }

        public async Task<string> GetBearerTokenAsync()
        {
            try
            {
                var clientId = _configuration["Salesforce:ClientId"];
                var clientSecret = _configuration["Salesforce:ClientSecret"];
                var username = _configuration["Salesforce:Username"];
                var password = _configuration["Salesforce:Password"];
                var tokenUrl = _configuration["Salesforce:BaseUrl"];

                var requestBody = new StringContent(
                    $"grant_type=password&client_id={clientId}&client_secret={clientSecret}&username={username}&password={password}",
                    Encoding.UTF8,
                    "application/x-www-form-urlencoded"
                );

                var response = await _httpClient.PostAsync(tokenUrl, requestBody);
                response.EnsureSuccessStatusCode();

                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<TokenResponse>(responseContent);

                return tokenResponse.access_token;

            }
            catch (Exception ex)
            {
                //_logger.LogError(ex, "Error occurred while retrieving Salesforce token.");
                throw new Exception("Failed to retrieve Salesforce token.", ex);
            }


        }
    }

    public class TokenResponse
    {
        public string access_token { get; set; }
        public string instance_url { get; set; }
        public string Id { get; set; }
        public string token_type { get; set; }
        public string issued_at { get; set; }
        public string Signature { get; set; }
    }
}
