namespace SalesforceWeb.Services.IServices
{
    public interface IOrganizationService
    {
        Task<T> GetAsync<T>(string id);
    }
}
