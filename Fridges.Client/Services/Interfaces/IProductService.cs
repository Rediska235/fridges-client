using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;

namespace Fridges.Client.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product> GetProductById(Guid? productId);
    Product CreateProduct(CreateProductDto product);
    Product UpdateProduct(UpdateProductDto updateProductDto);
    void DeleteProduct(Guid productID);
}
