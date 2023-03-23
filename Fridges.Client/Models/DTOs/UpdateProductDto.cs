using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models.DTOs;

public class UpdateProductDto
{
    public Guid Id { get; set; }

    [MaxLength(30)]
    public string Name { get; set; }

    public int? DefaultQuantity { get; set; }
}
