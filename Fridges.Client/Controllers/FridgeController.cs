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
        var fridges = _fridgeService.GetAllFridges().Result;
        return View(fridges);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFridgeById(Guid id)
    {
        var fridge = _fridgeService.GetFridgeById(id).Result;
        return View(fridge);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var fridgeModels = _fridgeModelService.GetAllFridgeModels().Result;
        ViewBag.FridgeModels = fridgeModels;
        ViewBag.Action = "Create";
        return View("_FridgeFormPartial");
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(FridgeDto fridgeDto)
    {
        if (ModelState.IsValid)
        {
            _fridgeService.CreateFridge(fridgeDto);
            var fridges = _fridgeService.GetAllFridges();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridges", fridges) });
        }

        var fridgeModels = _fridgeModelService.GetAllFridgeModels().Result;
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
        var fridgeModels = _fridgeModelService.GetAllFridgeModels().Result;
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
    public IActionResult Edit(FridgeDto fridgeDto)
    {
        if (ModelState.IsValid)
        {
            _fridgeService.UpdateFridge(fridgeDto);
            var fridges = _fridgeService.GetAllFridges();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridges", fridges) });
        }

        var fridgeModels = _fridgeModelService.GetAllFridgeModels().Result;
        ViewBag.FridgeModels = fridgeModels;
        ViewBag.Action = "Edit";

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_FridgeFormPartial", fridgeDto) });
    }

    [HttpPost("delete/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        _fridgeService.DeleteFridge(id);
        var fridges = _fridgeService.GetAllFridges();

        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridges", fridges) });
    }

    [HttpGet("{fridgeId}/products")]
    public async Task<IActionResult> AddProducts()
    {
        ViewBag.Products = _productService.GetAllProducts().Result;
        ViewBag.Action = "AddProducts";

        return View("_AddProductsPartial");
    }

    [HttpPost("{fridgeId:guid}/products")]
    public IActionResult AddProducts(AddProductsDto addProductsDto)
    {
        if (ModelState.IsValid)
        {
            _fridgeService.AddProducts(addProductsDto);
            var fridge = _fridgeService.GetFridgeById(addProductsDto.FridgeId).Result;

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetFridgeById", fridge) });
        }

        ViewBag.Products = _productService.GetAllProducts().Result;
        ViewBag.Action = "AddProducts";
        ViewBag.ProductName = _productService.GetProductById(addProductsDto.ProductId).Result.Name;

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_AddProductsPartial", addProductsDto) });
    }

    [HttpPost("{fridgeId:guid}/products/{productId:guid}/delete")]
    public IActionResult DeleteProducts(Guid fridgeId, Guid productId)
    {
        _fridgeService.RemoveProducts(fridgeId, productId);
        var fridge = _fridgeService.GetFridgeById(fridgeId).Result;

        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetFridgeById", fridge) });
    }
}