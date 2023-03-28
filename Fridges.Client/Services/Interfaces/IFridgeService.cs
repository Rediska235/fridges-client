using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;

namespace Fridges.Client.Services.Interfaces;

public interface IFridgeService
{
    Task<IEnumerable<Fridge>> GetAllFridges();
    Task<FridgeWithProductsDto> GetFridgeById(Guid? fridgeId);
    IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid fridgeId);
    Task AddProducts(AddProductsDto addProductsDto);
    Task RemoveProducts(Guid fridgeId, Guid productId);
    void UpdateProductsQuantity();
    Task<Fridge> CreateFridge(FridgeDto fridgeDto);
    Task<Fridge> UpdateFridge(FridgeDto fridgeDto);
    Task DeleteFridge(Guid fridgeID);
}
