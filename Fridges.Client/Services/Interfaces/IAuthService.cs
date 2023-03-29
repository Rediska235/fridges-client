using Fridges.Client.Models.DTOs;

namespace Fridges.Client.Services.Interfaces;

public interface IAuthService
{
    Task<string> Register(UserDto user);
    Task<string> Login(UserDto user);
    void Logout();
}
