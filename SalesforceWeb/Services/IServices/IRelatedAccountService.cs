using Microsoft.AspNetCore.Mvc;

namespace SalesforceWeb.Services.IServices
{
    public interface IRelatedAccountService
    {
        Task<string> GetRelatedAccount(string token, string id);
    }
}
