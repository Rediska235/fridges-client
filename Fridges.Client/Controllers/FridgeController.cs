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
        var fridge = _fridgeService.GetFridgeById(id);
        return View(fridge.Result);
    }

    [HttpGet("create")]
    public async Task<IActionResult> Create()
    {
        var fridgeModels = _fridgeModelService.GetAllFridgeModels();
        ViewBag.FridgeModels = fridgeModels.Result;
        return View();
    }

    [HttpPost("create")]
    public IActionResult Create(CreateFridgeDto createFridgeDto)
    {
        var fridge = _fridgeService.CreateFridge(createFridgeDto);
        GetAllFridges();
        return Ok(1);
    }

    [HttpGet("edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var fridgeModels = _fridgeModelService.GetAllFridgeModels();
        ViewBag.FridgeModels = fridgeModels.Result;
        var fridge = _fridgeService.GetFridgeById(id);
        ViewBag.Fridge = fridge.Result.Fridge;
        return View();
    }

    [HttpPost("edit/{id}")]
    public IActionResult Edit(UpdateFridgeDto updateFridgeDto)
    {
        var fridge = _fridgeService.UpdateFridge(updateFridgeDto);
        GetAllFridges();
        return Ok(2);
    }
}
