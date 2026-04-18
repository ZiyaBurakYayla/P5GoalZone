using GoalZone.API.DTOs.MatchDtos;
using GoalZone.API.Services;
using GoalZone.API.Services.SeasonServices;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;
        private readonly ISeasonService _seasonService;

        public MatchesController(IMatchService matchService, ISeasonService seasonService)
        {
            _matchService = matchService;
            _seasonService = seasonService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int? seasonId)
        {
            if (seasonId == null)
            {
                var active = await _seasonService.GetActiveAsync();
                if (active == null) return Ok(Enumerable.Empty<MatchListDto>());
                seasonId = active.Id;
            }
            var matches = await _matchService.GetAllMatchesAsync(seasonId.Value);
            return Ok(matches);
        }

        [HttpGet("live")]
        public async Task<IActionResult> GetLive()
        {
            var matches = await _matchService.GetLiveMatchesAsync();
            return Ok(matches);
        }

        [HttpGet("week/{weekNumber:int}")]
        public async Task<IActionResult> GetByWeek(int weekNumber, [FromQuery] int? seasonId)
        {
            if (seasonId == null)
            {
                var active = await _seasonService.GetActiveAsync();
                if (active == null) return Ok(Enumerable.Empty<MatchListDto>());
                seasonId = active.Id;
            }
            var matches = await _matchService.GetMatchesByWeekAsync(seasonId.Value, weekNumber);
            return Ok(matches);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetDetail(int id)
        {
            var match = await _matchService.GetMatchDetailAsync(id);
            if (match == null) return NotFound();
            return Ok(match);
        }
    }
}