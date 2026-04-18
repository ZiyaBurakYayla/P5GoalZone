using GoalZone.API.DTOs;
using GoalZone.API.Services;
using GoalZone.API.Services.TeamServices;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TeamsController : ControllerBase
    {
        private readonly ITeamService _service;
        public TeamsController(ITeamService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpPut("{id:int}/toggle")]
        public async Task<IActionResult> Toggle(int id)
        {
            await _service.ToggleActiveAsync(id);
            return NoContent();
        }
    }
}