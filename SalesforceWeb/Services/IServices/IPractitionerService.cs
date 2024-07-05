namespace SalesforceWeb.Services.IServices
{
    public interface IPractitionerService
    {
        Task<T> GetAsync<T>(string id);
    }
}
