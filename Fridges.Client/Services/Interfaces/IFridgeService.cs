﻿using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;

namespace Fridges.Application.Services.Interfaces;

public interface IFridgeService
{
    Task<IEnumerable<Fridge>> GetAllFridges();
    Task<FridgeWithProductsDto> GetFridgeById(Guid fridgeId);
    IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid fridgeId);
    void AddProducts(Guid fridgeId, AddProductsDto addProductsDto);
    void RemoveProducts(Guid fridgeId, Guid productId);
    void UpdateProductsQuantity();
    Fridge CreateFridge(CreateFridgeDto fridge);
    Fridge UpdateFridge(UpdateFridgeDto updateFridgeDto);
    void DeleteFridge(Guid fridgeID);
}