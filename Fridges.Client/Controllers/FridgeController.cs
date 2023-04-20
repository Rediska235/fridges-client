using Fridges.Client.Models.DTOs;
using Fridges.Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.Client.Controllers;

[Route("/fridges")]
public class FridgeController : Controller
{
    private readonly IFridgeService _fridgeService;
    private readonly IFridgeModelService _fridgeModelService;
    private readonly IProductService _productService;

    public FridgeController(IFridgeService fridgeService, IFridgeModelService fridgeModelService, IProductService productService)
    {
        _fridgeService = fridgeService;
        _fridgeModelService = fridgeModelService;
        _productService = productService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFridges()
    {
        var fridges = await _fridgeService.GetAllFridges();
        return View(fridges);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFridgeById(Guid id)
    {
        var fridge = await _fridgeService.GetFridgeById(id);
        return View(fridge);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var fridgeModels = await _fridgeModelService.GetAllFridgeModels();
        ViewBag.FridgeModels = fridgeModels;
        ViewBag.Action = "Create";
        return View("_FridgeFormPartial");
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(FridgeDto fridgeDto)
    {
        if (ModelState.IsValid)
        {
            await _fridgeService.CreateFridge(fridgeDto);
            var fridges = await _fridgeService.GetAllFridges();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridges", fridges) });
        }

        var fridgeModels = await _fridgeModelService.GetAllFridgeModels();
        ViewBag.FridgeModels = fridgeModels;
        ViewBag.Action = "Create";

        string fridgeModelName = null;
        if (fridgeDto.FridgeModelId != null)
        {
            fridgeModelName = fridgeModels.First(f => f.Id == fridgeDto.FridgeModelId).Name;
        }

        fridgeDto.FridgeModelName = fridgeModelName;

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_FridgeFormPartial", fridgeDto) });
    }

    [HttpGet("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var fridgeModels = await _fridgeModelService.GetAllFridgeModels();
        ViewBag.FridgeModels = fridgeModels;
        var fridge = _fridgeService.GetFridgeById(id).Result.Fridge;
        ViewBag.Action = "Edit";

        var fridgeDto = new FridgeDto()
        {
            Id = fridge.Id,
            Name = fridge.Name,
            OwnerName = fridge.OwnerName,
            FridgeModelId = fridge.FridgeModel.Id,
            FridgeModelName = fridge.FridgeModel.Name
        };

        return View("_FridgeFormPartial", fridgeDto);
    }

    [HttpPost("edit/{id:guid}")]
    public async Task<IActionResult> Edit(FridgeDto fridgeDto)
    {
        if (ModelState.IsValid)
        {
            await _fridgeService.UpdateFridge(fridgeDto);
            var fridges = await _fridgeService.GetAllFridges();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridges", fridges) });
        }

        var fridgeModels = await _fridgeModelService.GetAllFridgeModels();
        ViewBag.FridgeModels = fridgeModels;
        ViewBag.Action = "Edit";

        fridgeDto.FridgeModelName = fridgeModels.FirstOrDefault(fm => fm.Id == fridgeDto.FridgeModelId).Name;

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_FridgeFormPartial", fridgeDto) });
    }

    [HttpPost("delete/{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _fridgeService.DeleteFridge(id);
        var fridges = await _fridgeService.GetAllFridges();

        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridges", fridges) });
    }

    [HttpGet("{fridgeId}/products")]
    public async Task<IActionResult> AddProducts()
    {
        ViewBag.Products = await _productService.GetAllProducts();
        ViewBag.Action = "AddProducts";

        return View("_AddProductsPartial");
    }

    [HttpPost("{fridgeId:guid}/products")]
    public async Task<IActionResult> AddProducts(AddProductsDto addProductsDto)
    {
        if (ModelState.IsValid)
        {
            await _fridgeService.AddProducts(addProductsDto);
            var fridge = await _fridgeService.GetFridgeById(addProductsDto.FridgeId);

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetFridgeById", fridge) });
        }

        ViewBag.Products = await _productService.GetAllProducts();
        ViewBag.Action = "AddProducts";
        ViewBag.ProductName = _productService.GetProductById(addProductsDto.ProductId).Result.Name;

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_AddProductsPartial", addProductsDto) });
    }

    [HttpPost("{fridgeId:guid}/products/{productId:guid}/delete")]
    public async Task<IActionResult> DeleteProducts(Guid fridgeId, Guid productId)
    {
        await _fridgeService.RemoveProducts(fridgeId, productId);
        var fridge = await _fridgeService.GetFridgeById(fridgeId);

        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetFridgeById", fridge) });
    }

    [HttpPost("update-quantity")]
    public async Task<IActionResult> UpdateQuantity()
    {
        await _fridgeService.UpdateQuantity();

        return RedirectToAction("Index", "Home");
    }
}