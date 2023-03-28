using Fridges.Client.Models.DTOs;
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
        var fridge = _fridgeService.GetFridgeById(id).Result;
        return View(fridge);
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

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_FormPartial", fridgeDto) });
    }

    [HttpGet("edit/{id}")]
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

        return View("_FormPartial", fridgeDto);
    }

    [HttpPost("edit/{id}")]
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
        string a = Helper.RenderRazorViewToString(this, "_FormPartial", fridgeDto);

        return Json(new { isValid = false, html = a });
    }

    [HttpPost("delete/{id}")]
    public IActionResult Delete(Guid id)
    {
        _fridgeService.DeleteFridge(id);
        var fridges = _fridgeService.GetAllFridges();

        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridges", fridges) });
    }
}