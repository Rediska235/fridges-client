﻿using Fridges.Client.Services.Interfaces;
using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;
using Microsoft.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;

namespace Fridges.Client.Services.Implementations;

public class FridgeService : IFridgeService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private HttpClient _httpClient;

    public FridgeService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;

        _httpClient = _httpClientFactory.CreateClient("Fridges");
    }

    public async Task<IEnumerable<Fridge>> GetAllFridges()
    {
        IEnumerable<Fridge> fridges = new List<Fridge>();

        //var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
        //httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);

        var response = await _httpClient.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            fridges = response.Content.ReadFromJsonAsync<IEnumerable<Fridge>>().Result;
        }

        return fridges;
    }

    public async Task<FridgeWithProductsDto> GetFridgeById(Guid fridgeId)
    {
        var fridge = new FridgeWithProductsDto();

        var response = await _httpClient.GetAsync($"{fridgeId}");
        if (response.IsSuccessStatusCode)
        {
            fridge = response.Content.ReadFromJsonAsync<FridgeWithProductsDto>().Result;
        }

        return fridge;
    }

    public IEnumerable<ProductQuantity> GetProductsByFridgeId(Guid fridgeId)
    {
        throw new NotImplementedException();
    }

    public void AddProducts(Guid fridgeId, AddProductsDto addProductsDto)
    {
        throw new NotImplementedException();
    }

    public void RemoveProducts(Guid fridgeId, Guid productId)
    {
        throw new NotImplementedException();
    }
    
    public void UpdateProductsQuantity()
    {

        throw new NotImplementedException();
    }

    public async Task<Fridge> CreateFridge(CreateFridgeDto createFridgeDto)
    {
        var fridge = new Fridge();

        var uri = _httpClient.BaseAddress; 
        var json = JsonSerializer.Serialize(createFridgeDto);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = uri,
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };

        using var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            fridge = response.Content.ReadFromJsonAsync<Fridge>().Result;
        }

        return fridge;
    }

    public async Task<Fridge> UpdateFridge(UpdateFridgeDto updateFridgeDto)
    {
        var fridge = new Fridge();

        var uri = _httpClient.BaseAddress;
        var json = JsonSerializer.Serialize(updateFridgeDto);
        var request = new HttpRequestMessage
        {
            Method = HttpMethod.Put,
            RequestUri = uri,
            Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json")
        };

        using var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            fridge = response.Content.ReadFromJsonAsync<Fridge>().Result;
        }

        return fridge;
    }

    public void DeleteFridge(Guid fridgeId)
    {
        throw new NotImplementedException();
    }
}
