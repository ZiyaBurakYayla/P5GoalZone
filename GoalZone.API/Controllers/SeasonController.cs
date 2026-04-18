using GoalZone.API.DTOs.SeasonDtos;
using GoalZone.API.Services.SeasonServices;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class SeasonsController : ControllerBase
{
    private readonly ISeasonService _service;
    public SeasonsController(ISeasonService service) => _service = service;

    [HttpGet]
    public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

    [HttpGet("active")]
    public async Task<IActionResult> GetActive()
    {
        var season = await _service.GetActiveAsync();
        if (season == null) return NotFound();
        return Ok(season);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetById(int id)
    {
        var season = await _service.GetByIdAsync(id);
        if (season == null) return NotFound();
        return Ok(season);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateSeasonRequest request)
    {
        var season = await _service.CreateSeasonAsync(
            request.Name, request.StartYear, request.EndYear, request.IsActive);
        return CreatedAtAction(nameof(GetById), new { id = season.Id }, season);
    }

    [HttpPut("{id:int}/activate")]
    public async Task<IActionResult> Activate(int id)
    {
        var season = await _service.GetByIdAsync(id);
        if (season == null) return NotFound(new { error = "Season not found." });
        await _service.SetActiveSeasonAsync(id);
        return Ok(new { message = $"{season.Name} sezonu aktif yapıldı." });
    }
}