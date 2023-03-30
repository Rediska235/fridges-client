using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fridges.Client.Models.Entities;

public class Product
{
    public Guid? Id { get; set; }

    [Required]
    [MaxLength(30)]
    public string? Name { get; set; }

    [Required]
    public int? DefaultQuantity { get; set; }

    [JsonIgnore]
    public List<FridgeProduct>? FridgeProducts { get; set; }
}
