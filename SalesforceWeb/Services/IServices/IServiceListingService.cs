namespace SalesforceWeb.Services.IServices
{
    public interface IServiceListingService
    {
        Task<string> GetServiceListing(string token);
    }
}
