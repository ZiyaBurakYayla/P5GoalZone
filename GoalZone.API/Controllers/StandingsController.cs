using GoalZone.API.DTOs.StandingDtos;
using GoalZone.API.Services;
using GoalZone.API.Services.SeasonServices;
using GoalZone.API.Services.StandingServices;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StandingsController : ControllerBase
    {
        private readonly IStandingService _standingService;
        private readonly ISeasonService _seasonService;

        public StandingsController(IStandingService standingService, ISeasonService seasonService)
        {
            _standingService = standingService;
            _seasonService = seasonService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? seasonId)
        {
            if (seasonId == null)
            {
                var active = await _seasonService.GetActiveAsync();
                if (active == null) return Ok(Enumerable.Empty<StandingDto>());
                seasonId = active.Id;
            }
            var standings = await _standingService.GetStandingsAsync(seasonId.Value);
            return Ok(standings);
        }
    }
}