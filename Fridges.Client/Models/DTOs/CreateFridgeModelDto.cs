using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models.DTOs;

public class CreateFridgeModelDto
{
    [MaxLength(50)]
    public string Name { get; set; }

    public int? Year { get; set; }
}
