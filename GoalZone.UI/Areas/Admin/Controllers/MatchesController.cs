using GoalZone.UI.Models.Admin;
using GoalZone.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

[Area("Admin")]
public class MatchesController : Controller
{
    private readonly ApiClient _api;
    public MatchesController(ApiClient api) => _api = api;

    private bool IsLoggedIn => HttpContext.Session.GetString("AdminLoggedIn") == "true";

    public async Task<IActionResult> Index([FromQuery] int? seasonId)
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
        var allSeasons = await _api.GetSeasonsAsync();
        var activeSeason = await _api.GetActiveSeasonAsync();
        var selectedId = seasonId ?? activeSeason?.Id;
        var selected = allSeasons.FirstOrDefault(s => s.Id == selectedId) ?? activeSeason;
        var matches = await _api.GetMatchesAsync(selected?.Id);
        var allTeams = await _api.GetTeamsAsync();
        ViewBag.Season = selected;
        ViewBag.AllSeasons = allSeasons;
        ViewBag.AllTeams = allTeams;
        return View(matches);
    }

    public async Task<IActionResult> Create()
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
        var vm = new AdminCreateMatchViewModel
        {
            Teams = await _api.GetTeamsAsync(),
            Seasons = await _api.GetSeasonsAsync(),
            Referees = await _api.GetRefereesAsync()
        };
        return View(vm);
    }

    [HttpPost]
    public async Task<IActionResult> Create(int seasonId, int homeTeamId, int awayTeamId,
        DateTime matchDate, string? venue, int? refereeId, int weekNumber, bool isFeatured)
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
        var req = new { seasonId, weekNumber, homeTeamId, awayTeamId, matchDate, venue, refereeId, isFeatured };
        var matchId = await _api.CreateMatchAsync(req);
        if (matchId.HasValue)
            return RedirectToAction("Score", new { id = matchId.Value });
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Score(int id)
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
        var match = await _api.GetMatchDetailAsync(id);
        if (match == null) return NotFound();
        return View(match);
    }

    [HttpPost]
    public async Task<IActionResult> Score(int id, int? homeScoreHT, int? awayScoreHT,
        int? homeScoreFT, int? awayScoreFT, string status, int? currentMinute)
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
        var req = new { homeScoreHT, awayScoreHT, homeScoreFT, awayScoreFT, status, currentMinute };
        await _api.UpdateScoreAsync(id, req);
        return RedirectToAction("Events", new { id });
    }

    public async Task<IActionResult> Events(int id)
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
        var match = await _api.GetMatchDetailAsync(id);
        if (match == null) return NotFound();
        return View(match);
    }

    [HttpPost]
    public async Task<IActionResult> AddEvent(int id, bool isHomeTeam, string eventType,
        int minute, string? playerName, string? substitutedPlayerName, string? description)
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
        var req = new { isHomeTeam, eventType, minute, playerName, substitutedPlayerName, description };
        await _api.AddEventAsync(id, req);
        return RedirectToAction("Events", new { id });
    }

    [HttpPost]
    public async Task<IActionResult> DeleteEvent(int eventId, int matchId)
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
        await _api.DeleteEventAsync(eventId);
        return RedirectToAction("Events", new { id = matchId });
    }

    public async Task<IActionResult> Statistics(int id)
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
        var match = await _api.GetMatchDetailAsync(id);
        if (match == null) return NotFound();
        return View(match);
    }

    [HttpPost]
    public async Task<IActionResult> Statistics(int id, decimal? homePossession, decimal? awayPossession,
        int? homeTotalShots, int? awayTotalShots, int? homeShotsOnTarget, int? awayShotsOnTarget,
        int? homePasses, int? awayPasses, decimal? homePassAccuracy, decimal? awayPassAccuracy,
        int? homeCorners, int? awayCorners, int? homeFouls, int? awayFouls,
        int? homeOffsides, int? awayOffsides, int? homeYellowCards, int? awayYellowCards,
        int? homeRedCards, int? awayRedCards, string? halfTimeScore, string? secondHalfScore)
    {
        if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
        var req = new
        {
            halfTimeScore,
            secondHalfScore,
            homePossession,
            awayPossession,
            homeTotalShots,
            awayTotalShots,
            homeShotsOnTarget,
            awayShotsOnTarget,
            homePasses,
            awayPasses,
            homePassAccuracy,
            awayPassAccuracy,
            homeCorners,
            awayCorners,
            homeFouls,
            awayFouls,
            homeOffsides,
            awayOffsides,
            homeYellowCards,
            awayYellowCards,
            homeRedCards,
            awayRedCards
        };
        await _api.UpsertStatisticsAsync(id, req);
        return RedirectToAction("Events", new { id });
    }
}