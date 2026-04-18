using GoalZone.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class TeamsController : Controller
    {
        private readonly ApiClient _api;
        public TeamsController(ApiClient api) => _api = api;

        private bool IsLoggedIn => HttpContext.Session.GetString("AdminLoggedIn") == "true";

        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
            var teams = await _api.GetTeamsAsync();
            return View(teams);
        }

        [HttpPost]
        public async Task<IActionResult> Toggle(int teamId)
        {
            if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
            await _api.ToggleTeamActiveAsync(teamId);
            return RedirectToAction("Index");
        }
    }
}
