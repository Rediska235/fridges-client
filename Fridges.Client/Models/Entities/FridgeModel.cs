using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models.Entities;

public class FridgeModel
{
    public Guid? Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string? Name { get; set; }

    [Required]
    public int? Year { get; set; }
}
