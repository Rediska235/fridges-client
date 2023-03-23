using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fridges.Client.Models.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(30)]
    public string Name { get; set; }

    public int? DefaultQuantity { get; set; }

    [JsonIgnore]
    public List<FridgeProduct> FridgeProducts { get; set; }
}
