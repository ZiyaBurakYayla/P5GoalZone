using GoalZone.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.WebUI.ViewComponents
{
    public class LiveScoreboardViewComponent : ViewComponent
    {
        private readonly ApiClient _api;
        public LiveScoreboardViewComponent(ApiClient api) => _api = api;

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var matches = await _api.GetLiveMatchesAsync();
            return View(matches);
        }
    }
}