using SalesforceAPI.Models;
using SalesforceAPI.Dtos;

namespace SalesforceAPI.Repository.IRepostiory
{
    public interface IUserRepository
    {
        bool IsUniqueUser(string username);
        Task<LoginResponseDto> Login(LoginRequestDto loginRequestDto);
        Task<bool> LogOutAuditEntry(string userName);
        Task<UserDto> Register(RegisterationRequestDto registerationRequestDto);
    }
}
