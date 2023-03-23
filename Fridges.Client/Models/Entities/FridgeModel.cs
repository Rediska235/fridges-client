using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models.Entities;

public class FridgeModel
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }

    public int? Year { get; set; }
}
