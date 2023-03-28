using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;
using Fridges.Client.Services.Interfaces;

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

    public Product CreateProduct(CreateProductDto createProductDto)
    {
        throw new NotImplementedException();
    }

    public Product UpdateProduct(UpdateProductDto updateProductDto)
    {
        throw new NotImplementedException();
    }

    public void DeleteProduct(Guid productId)
    {
        throw new NotImplementedException();
    }
}
