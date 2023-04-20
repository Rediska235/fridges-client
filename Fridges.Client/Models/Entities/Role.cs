using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fridges.Client.Models.Entities;

public class Role
{
    public Guid Id { get; set; }

    [MaxLength(30)]
    public string Title { get; set; }

    [JsonIgnore]
    public List<User> Users { get; set; }
}
