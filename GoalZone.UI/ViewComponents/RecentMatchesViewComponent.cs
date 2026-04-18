using GoalZone.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.WebUI.ViewComponents
{
    public class RecentMatchesViewComponent : ViewComponent
    {
        private readonly ApiClient _api;
        public RecentMatchesViewComponent(ApiClient api) => _api = api;

        public async Task<IViewComponentResult> InvokeAsync(int? seasonId = null)
        {
            var all = await _api.GetMatchesAsync(seasonId);
            var recent = all
                .Where(m => m.Status == "Finished")
                .OrderByDescending(m => m.MatchDate)
                .Take(5)
                .ToList();
            return View(recent);
        }
    }
}