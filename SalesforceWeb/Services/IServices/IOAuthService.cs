using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace SalesforceWeb.Services.IServices
{
    public interface IOAuthService
    {
        Task<string> GetBearerTokenAsync();
    }
}
