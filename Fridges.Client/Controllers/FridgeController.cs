using Fridges.Client.Models.DTOs;
using Fridges.Client.Models.Entities;
using Fridges.Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.Client.Controllers;

[Route("/fridges")]
public class FridgeController : Controller
{
    private readonly IFridgeService _fridgeService;
    private readonly IFridgeModelService _fridgeModelService;

    public FridgeController(IFridgeService fridgeService, IFridgeModelService fridgeModelService)
    {
        _fridgeService = fridgeService;
        _fridgeModelService = fridgeModelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFridges()
    {
        var fridges = _fridgeService.GetAllFridges();
        return View(fridges.Result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetFridgeById(Guid id)
    {
        var fridge = _fridgeService.GetFridgeById(id);
        return View(fridge.Result);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var fridgeModels = _fridgeModelService.GetAllFridgeModels();
        ViewBag.FridgeModels = fridgeModels.Result;
        ViewBag.Action = "Create";
        return View("_FormPartial");
    }

    [HttpPost("create")]
    public IActionResult Create(CreateFridgeDto createFridgeDto)
    {
        if (ModelState.IsValid)
        {
            _fridgeService.CreateFridge(createFridgeDto);
            var fridgeModels = _fridgeModelService.GetAllFridgeModels();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridges", fridgeModels) });
        }

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_FormPartial", createFridgeDto) });
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var fridgeModels = _fridgeModelService.GetAllFridgeModels();
        ViewBag.FridgeModels = fridgeModels.Result;
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
        return View("_FormPartial", fridgeDto);
    }

    [HttpPost("edit/{id}")]
    public IActionResult Edit(UpdateFridgeDto updateFridgeDto)
    {
        if (ModelState.IsValid)
        {
            _fridgeService.UpdateFridge(updateFridgeDto);
            var fridgeModels = _fridgeModelService.GetAllFridgeModels();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridges", fridgeModels) });
        }

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_FormPartial", updateFridgeDto) });
    }

    [HttpPost("delete/{id}")]
    public IActionResult Delete(Guid id)
    {
        _fridgeService.DeleteFridge(id);
        var fridgeModels = _fridgeModelService.GetAllFridgeModels();

        return Json(new { html = Helper.RenderRazorViewToString(this, "GetAllFridges", fridgeModels) });
    }
}