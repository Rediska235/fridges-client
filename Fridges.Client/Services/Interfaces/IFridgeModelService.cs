using Fridges.Client.Models.Entities;

namespace Fridges.Client.Services.Interfaces;

public interface IFridgeModelService
{
    Task<IEnumerable<FridgeModel>> GetAllFridgeModels();
    Task<FridgeModel> GetFridgeModelById(Guid? fridgeModelId);
    Task<string> CreateFridgeModel(FridgeModel fridgeModel);
    Task<string> UpdateFridgeModel(FridgeModel fridgeModel);
    Task DeleteFridgeModel(Guid fridgeModelId);
}
