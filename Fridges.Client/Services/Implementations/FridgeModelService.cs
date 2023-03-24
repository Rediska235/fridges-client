using Fridges.Client.Models.Entities;
using Fridges.Client.Services.Interfaces;

namespace Fridges.Client.Services.Implementations;

public class FridgeModelService : IFridgeModelService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private HttpClient _httpClient;

    public FridgeModelService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;

        _httpClient = _httpClientFactory.CreateClient("FridgeModels");
    }

    public async Task<IEnumerable<FridgeModel>> GetAllFridgeModels()
    {
        IEnumerable<FridgeModel> fridgeModels = new List<FridgeModel>();

        //var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
        //httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);

        var response = await _httpClient.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            fridgeModels = response.Content.ReadFromJsonAsync<IEnumerable<FridgeModel>>().Result;
        }

        return fridgeModels;
    }
}
