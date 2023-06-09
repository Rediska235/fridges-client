﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Fridges.Client.Models.Entities;

public class Fridge
{
    [Key]
    public Guid Id { get; set; }

    [MaxLength(50)]
    public string Name { get; set; } = "";

    [MaxLength(30)]
    public string? OwnerName { get; set; } = "";

    public FridgeModel FridgeModel { get; set; }

    [JsonIgnore]
    public List<FridgeProduct> FridgeProducts { get; set; }

    public Fridge()
    {
        Id = Guid.NewGuid();
        Name = "";
        OwnerName = "";
        FridgeModel = null;
        FridgeProducts = null;
    }
}
