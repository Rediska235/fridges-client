using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;

namespace Fridges.Client.Services.Interfaces;

public interface IAuthService
{
    Task<string> Register(UserDto user);
    Task<string> Login(UserDto user);
    void Logout();
    Task GiveRoles(GiveRoleDto giveRoleDto);
    Task<IEnumerable<UserOutputDto>> GetAllUsers();
    Task<IEnumerable<Role>> GetAllRoles();
}
