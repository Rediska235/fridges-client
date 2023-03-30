using Fridges.Client.Models.Entities;
using Fridges.Client.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.Client.Controllers;

[Route("fridge-model")]
public class FridgeModelController : Controller
{
    private readonly IFridgeModelService _fridgeModelService;

    public FridgeModelController(IFridgeModelService fridgeModelService)
    {
        _fridgeModelService = fridgeModelService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllFridgeModels()
    {
        var fridgeModels = _fridgeModelService.GetAllFridgeModels().Result;
        return View(fridgeModels);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        ViewBag.Action = "Create";
        return View("_FridgeModelFormPartial");
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create(FridgeModel fridgeModel)
    {
        if (ModelState.IsValid)
        {
            _fridgeModelService.CreateFridgeModel(fridgeModel);
            var fridgeModels = _fridgeModelService.GetAllFridgeModels();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridgeModels", fridgeModels) });
        }

        ViewBag.Action = "Create";

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_FridgeModelFormPartial", fridgeModel) });
    }

    [HttpGet("edit/{id:guid}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var product = _fridgeModelService.GetFridgeModelById(id).Result;
        ViewBag.Action = "Edit";

        return View("_FridgeModelFormPartial", product);
    }

    [HttpPost("edit/{id:guid}")]
    public async Task<IActionResult> Edit(FridgeModel fridgeModel)
    {
        if (ModelState.IsValid)
        {
            _fridgeModelService.UpdateFridgeModel(fridgeModel);
            var fridgeModels = _fridgeModelService.GetAllFridgeModels();

            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridgeModels", fridgeModels) });
        }

        ViewBag.Action = "Edit";

        return Json(new { isValid = false, html = Helper.RenderRazorViewToString(this, "_FridgeModelFormPartial", fridgeModel) });
    }

    [HttpPost("delete/{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        _fridgeModelService.DeleteFridgeModel(id);
        var products = _fridgeModelService.GetAllFridgeModels();

        return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "GetAllFridgeModels", products) });
    }
}
