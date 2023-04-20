using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models.DTOs;

public class CreateProductDto
{
    [MaxLength(30)]
    public string Name { get; set; }

    public int? DefaultQuantity { get; set; }
}
