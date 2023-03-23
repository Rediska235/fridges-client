using Fridges.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fridges.Client.Controllers;

[Route("/fridges")]
public class FridgeController : Controller
{
    private readonly IFridgeService _fridgeService;

    public FridgeController(IFridgeService fridgeService)
    {
        _fridgeService = fridgeService;
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
}
