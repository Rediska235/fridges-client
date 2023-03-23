using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models.Entities;

public class User
{
    public Guid Id { get; set; }

    [MaxLength(30)]
    public string Username { get; set; }

    [MaxLength(60)]
    public string PasswordHash { get; set; }

    [MaxLength(300)]
    public string RefreshToken { get; set; } = "";

    public DateTime TokenCreated { get; set; }

    public DateTime TokenExpires { get; set; }

    public List<Role> Roles { get; set; } = new List<Role>();
}
