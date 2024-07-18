namespace SalesforceWeb.Services.IServices
{
    public interface ISalesforceService
    {
        Task<string> PostToCompositeApiAsync(string jsonBody);
    }
}
