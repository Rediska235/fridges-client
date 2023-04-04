using Fridges.Client.Models.Entities;

namespace Fridges.Client.Models.DTOs;

public class FridgeWithProductsDto
{
    public Fridge Fridge { get; set; }

    public IEnumerable<ProductQuantity> Products { get; set; }
}
