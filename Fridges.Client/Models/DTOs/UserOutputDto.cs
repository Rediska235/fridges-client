using Fridges.Client.Models.Entities;

namespace Fridges.Client.Models.DTOs;

public class UserOutputDto
{
    public string Username { get; set; }

    public List<Role> Roles { get; set; } = new List<Role>();
}
