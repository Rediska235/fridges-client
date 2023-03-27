using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models.DTOs;

public class FridgeDto
{
    public Guid? Id { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2)]
    public string Name { get; set; }

    [Required]
    [StringLength(30, MinimumLength = 2)]
    public string? OwnerName { get; set; }

    [Required(ErrorMessage = "Fridge model is required")]
    public Guid? FridgeModelId { get; set; }

    public string? FridgeModelName { get; set; }
}
