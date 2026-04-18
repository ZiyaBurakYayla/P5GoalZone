using GoalZone.UI.Models;
using GoalZone.UI.Models.Components;
using GoalZone.UI.Models.Pages;
using GoalZone.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.WebUI.Controllers
{
    public class StandingsController : Controller
    {
        private readonly ApiClient _api;
        public StandingsController(ApiClient api) => _api = api;

        public async Task<IActionResult> Index([FromQuery] int? seasonId)
        {
            var allSeasons = await _api.GetSeasonsAsync();
            var activeSeason = await _api.GetActiveSeasonAsync();

            var selectedId = seasonId ?? activeSeason?.Id;
            var selected = allSeasons.FirstOrDefault(s => s.Id == selectedId) ?? activeSeason;

            var standings = await _api.GetStandingsAsync(selected?.Id);

            var vm = new StandingsPageViewModel
            {
                Standings = standings,
                ActiveSeason = selected,
                AllSeasons = allSeasons,
                SelectedSeasonId = selected?.Id ?? 0
            };
            return View(vm);
        }
    }
}