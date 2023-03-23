using Fridges.Client.Models.Entities;

namespace Fridges.Client.Models.DTOs;

public class ProductQuantity
{
    public Product Product { get; set; }

    public int Quanity { get; set; }
}
