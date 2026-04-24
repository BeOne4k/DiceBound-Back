using DiceBound.DTOs.Combat;
using DiceBound.Interfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class CombatController : ControllerBase
{
    private readonly ICombatService _combatService;

    public CombatController(ICombatService combatService)
    {
        _combatService = combatService;
    }

    [HttpPost("start")]
    public async Task<IActionResult> Start(StartCombatDto dto)
    {
        var result = await _combatService.StartCombat(dto);
        return Ok(result);
    }
}