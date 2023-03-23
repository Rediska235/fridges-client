using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models.Entities;

public class FridgeProduct
{
    [Key]
    public Guid Id { get; set; }

    public Fridge Fridge { get; set; }

    public Product Product { get; set; }

    public int Quantity { get; set; }
}
