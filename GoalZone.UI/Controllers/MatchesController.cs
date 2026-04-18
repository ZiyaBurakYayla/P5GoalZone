using GoalZone.UI.Models.Components;
using GoalZone.UI.Models.Pages;
using GoalZone.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.WebUI.Controllers
{
    public class MatchesController : Controller
    {
        private readonly ApiClient _api;
        public MatchesController(ApiClient api) => _api = api;

        public async Task<IActionResult> Index([FromQuery] int? seasonId)
        {
            var allSeasons = await _api.GetSeasonsAsync();
            var activeSeason = await _api.GetActiveSeasonAsync();

            var selectedId = seasonId ?? activeSeason?.Id;
            var selected = allSeasons.FirstOrDefault(s => s.Id == selectedId) ?? activeSeason;

            var all = await _api.GetMatchesAsync(selected?.Id);
            var byWeek = all.GroupBy(m => m.WeekNumber).OrderBy(g => g.Key).ToList();

            var currentWeek = all
                .Where(m => m.Status == "InProgress" || m.Status == "HalfTime")
                .Select(m => m.WeekNumber)
                .DefaultIfEmpty(byWeek.LastOrDefault()?.Key ?? 1)
                .First();

            var vm = new FixturePageViewModel
            {
                MatchesByWeek = byWeek,
                CurrentWeek = currentWeek,
                ActiveSeason = selected,
                AllSeasons = allSeasons,
                SelectedSeasonId = selected?.Id ?? 0
            };
            return View(vm);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var match = await _api.GetMatchDetailAsync(id);
            if (match == null) return NotFound();
            return View(match);
        }
    }
}