namespace SalesforceWeb.Services.IServices
{
    public interface IPractitionerService
    {
        Task<T> GetAsync<T>(string id, string token);
        Task<T> EditAsync<T>(string id, string token);
    }
}
