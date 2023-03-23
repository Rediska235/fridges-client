namespace Fridges.Client.Models.DTOs;

public class RemoveProductsDto
{
    public Guid FridgeId { get; set; }

    public Guid ProductId { get; set; }
}
