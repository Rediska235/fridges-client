using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;
using Fridges.Client.Services.Interfaces;
using Microsoft.CodeAnalysis;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

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

        var response = await _httpClient.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            fridgeModels = response.Content.ReadFromJsonAsync<IEnumerable<FridgeModel>>().Result;
        }

        return fridgeModels;
    }

    public async Task<FridgeModel> GetFridgeModelById(Guid? fridgeModelId)
    {
        FridgeModel fridgeModel = new FridgeModel();

        var response = await _httpClient.GetAsync($"{fridgeModelId}");
        if (response.IsSuccessStatusCode)
        {
            fridgeModel = response.Content.ReadFromJsonAsync<FridgeModel>().Result;
        }

        return fridgeModel;
    }

    public async Task<string> CreateFridgeModel(FridgeModel fridgeModel)
    {
        var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);

        var uri = _httpClient.BaseAddress;

        var createFridgeModelDto = new CreateFridgeModelDto()
        {
            Name = fridgeModel.Name,
            Year = fridgeModel.Year
        };

        var json = JsonSerializer.Serialize(createFridgeModelDto);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };

        using var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return String.Empty;
        }

        string errorMessage = await response.Content.ReadAsStringAsync();
        errorMessage = errorMessage.Substring(10, errorMessage.Length - 12);

        return errorMessage;
    }

    public async Task<string> UpdateFridgeModel(FridgeModel fridgeModel)
    {
        var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);

        var uri = _httpClient.BaseAddress;

        var updateFridgeModelDto = new UpdateFridgeModelDto()
        {
            Id = fridgeModel.Id,
            Name = fridgeModel.Name,
            Year = fridgeModel.Year
        };

        var json = JsonSerializer.Serialize(updateFridgeModelDto);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = uri,
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };

        using var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            return String.Empty;
        }

        string errorMessage = await response.Content.ReadAsStringAsync();
        errorMessage = errorMessage.Substring(10, errorMessage.Length - 12);

        return errorMessage;
    }

    public async Task DeleteFridgeModel(Guid fridgeModelId)
    {
        var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);

        var uri = _httpClient.BaseAddress + fridgeModelId.ToString();
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);

        using var response = await _httpClient.SendAsync(request);
    }
}
