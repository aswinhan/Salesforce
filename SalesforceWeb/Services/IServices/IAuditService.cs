namespace SalesforceWeb.Services.IServices
{
    public interface IAuditService
    {
        Task<T> GetAsync<T>(string token);
    }
}
