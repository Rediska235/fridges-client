using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models.DTOs;

public class AddProductsDto
{
    [Required]
    public Guid? FridgeId { get; set; }

    [Required(ErrorMessage = "Product is required")]
    public Guid? ProductId { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    public int Quantity { get; set; }
}
