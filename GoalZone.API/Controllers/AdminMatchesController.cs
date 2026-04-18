using GoalZone.API.DTOs.MatchDtos;
using GoalZone.API.DTOs.MatchEventDtos;
using GoalZone.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.API.Controllers
{
    [ApiController]
    [Route("api/admin/matches")]
    public class AdminMatchesController : ControllerBase
    {
        private readonly IMatchService _matchService;
        public AdminMatchesController(IMatchService matchService) => _matchService = matchService;

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateMatchRequest request)
        {
            var id = await _matchService.CreateMatchAsync(request);
            return CreatedAtAction(nameof(MatchesController.GetDetail), "Matches", new { id }, new { id });
        }

        [HttpPut("{id:int}/score")]
        public async Task<IActionResult> UpdateScore(int id, [FromBody] UpdateScoreRequest request)
        {
            try { await _matchService.UpdateScoreAsync(id, request); return NoContent(); }
            catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
        }

        [HttpPost("{id:int}/events")]
        public async Task<IActionResult> AddEvent(int id, [FromBody] AddMatchEventRequest request)
        {
            try { await _matchService.AddMatchEventAsync(id, request); return Ok(new { message = "Event added." }); }
            catch (ArgumentException ex) { return BadRequest(new { error = ex.Message }); }
        }

        [HttpDelete("events/{eventId:int}")]
        public async Task<IActionResult> DeleteEvent(int eventId)
        {
            try { await _matchService.DeleteMatchEventAsync(eventId); return NoContent(); }
            catch (KeyNotFoundException ex) { return NotFound(new { error = ex.Message }); }
        }

        [HttpPut("{id:int}/statistics")]
        public async Task<IActionResult> UpsertStatistics(int id, [FromBody] UpsertStatisticRequest request)
        {
            await _matchService.UpsertStatisticAsync(id, request);
            return NoContent();
        }
    }
}