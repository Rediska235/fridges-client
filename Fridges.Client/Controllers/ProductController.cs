using Fridges.Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.Client.Controllers;

[Route("/products")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService fridgeService)
    {
        _productService = fridgeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = _productService.GetAllProducts();
        return View(products.Result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetProductById(Guid id)
    {
        var fridge = _productService.GetProductById(id);
        return View(fridge.Result);
    }
}
