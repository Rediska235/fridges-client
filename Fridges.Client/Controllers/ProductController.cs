using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;
using Fridges.Client.Services.Implementations;
using Fridges.Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.Client.Controllers;

[Route("/products")]
public class ProductController : Controller
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = _productService.GetAllProducts().Result;
        return View(products);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Action = "Create";
        return View("_ProductFormPartial");
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(Product product)
    {
        if (ModelState.IsValid)
        {
            await _productService.CreateProduct(product);
            var products = _productService.GetAllProducts();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllProducts", products) });
        }

        ViewBag.Action = "Create";

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_ProductFormPartial", product) });
    }

    [HttpGet("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var product = _productService.GetProductById(id).Result;
        ViewBag.Action = "Edit";

        return View("_ProductFormPartial", product);
    }

    [HttpPost("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Product product)
    {
        if (ModelState.IsValid)
        {
            await _productService.UpdateProduct(product);
            var products = _productService.GetAllProducts();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllProducts", products) });
        }

        ViewBag.Action = "Edit";

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_ProductFormPartial", product) });
    }

    [HttpPost("delete/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        _productService.DeleteProduct(id);
        var products = _productService.GetAllProducts();

        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllProducts", products) });
    }
}
