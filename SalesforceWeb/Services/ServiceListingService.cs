using SalesforceWeb.Services.IServices;
using System.Net.Http.Headers;

namespace SalesforceWeb.Services
{
    public class ServiceListingService : IServiceListingService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<ServiceListingService> _logger;

        public ServiceListingService(HttpClient httpClient, IConfiguration configuration, ILogger<ServiceListingService> logger)
        {
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<string> GetServiceListing(string token)
        {
            try
            {
                _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var apiUrl = _configuration["Salesforce:BaseUrl"];
                var query = "SELECT Id, Name FROM Service_Listing__c WHERE is_Certification__c = true AND Active__c = true";
                var requestUri = $"{apiUrl}/services/data/v59.0/query/?q={Uri.EscapeDataString(query)}";
                var response = await _httpClient.GetAsync(requestUri);

                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    _logger.LogError("Failed to fetch data from Salesforce API: {ReasonPhrase}", response.ReasonPhrase);
                    throw new HttpRequestException($"Failed to fetch data from Salesforce API: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching service listing. {errorMessage}", ex.Message);
                throw;
            }
        }
    }
}
