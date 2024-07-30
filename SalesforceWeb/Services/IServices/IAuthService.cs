using SalesforceWeb.Dtos;

namespace SalesforceWeb.Services.IServices
{
    public interface IAuthService
    {
        Task<T> LoginAsync<T>(LoginRequestDto objToCreate);
        Task<T> RegisterAsync<T>(RegisterationRequestDto objToCreate);
        Task<T> LogOut<T>(string token);
    }
}
