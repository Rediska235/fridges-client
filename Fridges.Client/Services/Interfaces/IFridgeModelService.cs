using Fridges.Client.Models.Entities;

namespace Fridges.Client.Services.Interfaces;

public interface IFridgeModelService
{
    Task<IEnumerable<FridgeModel>> GetAllFridgeModels();
}
