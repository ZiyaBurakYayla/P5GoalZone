using GoalZone.UI.Models.Components;
using GoalZone.UI.Models.Pages;
using GoalZone.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApiClient _api;
        public HomeController(ApiClient api) => _api = api;

        public async Task<IActionResult> Index([FromQuery] int? seasonId)
        {
            var allSeasons = await _api.GetSeasonsAsync();
            var activeSeason = await _api.GetActiveSeasonAsync();

            var selectedId = seasonId ?? activeSeason?.Id;
            var selected = allSeasons.FirstOrDefault(s => s.Id == selectedId) ?? activeSeason;

            var allMatches = await _api.GetMatchesAsync(selected?.Id);

            var vm = new HomePageViewModel
            {
                ActiveSeason = selected,
                AllSeasons = allSeasons,
                LiveMatches = allMatches.Where(m => m.Status == "InProgress").ToList(),
                RecentMatches = allMatches.Where(m => m.Status == "Finished")
                                    .OrderByDescending(m => m.MatchDate).Take(5).ToList(),
                UpcomingMatches = allMatches.Where(m => m.Status == "NotPlayed")
                                    .OrderBy(m => m.MatchDate).Take(5).ToList(),
                FeaturedMatch = allMatches.FirstOrDefault(m => m.IsFeatured && m.Status == "Finished")
            };

            return View(vm);
        }
    }
}