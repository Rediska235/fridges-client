﻿using System.ComponentModel.DataAnnotations;

namespace Fridges.Client.Models.DTOs;

public class UpdateFridgeDto
{
    public Guid? Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; }

    [MaxLength(30)]
    public string? OwnerName { get; set; }

    public Guid? FridgeModelId { get; set; }
}
