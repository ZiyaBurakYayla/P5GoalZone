using GoalZone.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.WebUI.ViewComponents
{
    public class StandingsTableViewComponent : ViewComponent
    {
        private readonly ApiClient _api;
        public StandingsTableViewComponent(ApiClient api) => _api = api;

        public async Task<IViewComponentResult> InvokeAsync(int? seasonId = null)
        {
            var standings = await _api.GetStandingsAsync(seasonId);
            return View(standings);
        }
    }
}