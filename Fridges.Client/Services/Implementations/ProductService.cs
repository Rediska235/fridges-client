using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;
using Fridges.Client.Services.Interfaces;
using Microsoft.Net.Http.Headers;
using System.Text.Json;

namespace Fridges.Client.Services.Implementations;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private HttpClient _httpClient;

    public ProductService(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor)
    {
        _httpClientFactory = httpClientFactory;
        _httpContextAccessor = httpContextAccessor;

        _httpClient = _httpClientFactory.CreateClient("Products");
    }

    public async Task<IEnumerable<Product>> GetAllProducts()
    {
        IEnumerable<Product> products = new List<Product>();

        var response = await _httpClient.GetAsync("");
        if (response.IsSuccessStatusCode)
        {
            products = response.Content.ReadFromJsonAsync<IEnumerable<Product>>().Result;
        }

        return products;
    }

    public async Task<Product> GetProductById(Guid? productId)
    {
        var product = new Product();

        if(productId == null)
        {
            return product;
        }

        var response = await _httpClient.GetAsync($"{productId}");
        if (response.IsSuccessStatusCode)
        {
            product = response.Content.ReadFromJsonAsync<Product>().Result;
        }

        return product;
    }

    public async Task<string> CreateProduct(Product product)
    {
        var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);

        var uri = _httpClient.BaseAddress;

        var createProductDto = new CreateProductDto()
        {
            Name = product.Name,
            DefaultQuantity = product.DefaultQuantity
        };

        var json = JsonSerializer.Serialize(createProductDto);
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

    public async Task<string> UpdateProduct(Product product)
    {
        var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);

        var uri = _httpClient.BaseAddress;

        var updateProductDto = new UpdateProductDto()
        {
            Id = product.Id,
            Name = product.Name,
            DefaultQuantity = product.DefaultQuantity
        };

        var json = JsonSerializer.Serialize(updateProductDto);
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

    public async Task DeleteProduct(Guid productId)
    {
        var jwtToken = _httpContextAccessor.HttpContext.Session.GetString("jwtToken");
        _httpClient.DefaultRequestHeaders.Add(HeaderNames.Authorization, "Bearer " + jwtToken);

        var uri = _httpClient.BaseAddress + productId.ToString();
        var request = new HttpRequestMessage(HttpMethod.Delete, uri);

        using var response = await _httpClient.SendAsync(request);
    }
}
