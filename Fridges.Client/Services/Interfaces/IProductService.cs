using Fridges.Client.Models.Entities;

namespace Fridges.Client.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductById(Guid? productId);
    Task<string> CreateProduct(Product product);
    Task<string> UpdateProduct(Product product);
    Task DeleteProduct(Guid productID);
}
