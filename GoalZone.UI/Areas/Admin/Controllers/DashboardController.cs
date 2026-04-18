using GoalZone.UI.Models.Components;
using GoalZone.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.WebUI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly ApiClient _api;
        public DashboardController(ApiClient api) => _api = api;

        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password)
        {
            var ok = await _api.LoginAsync(username, password);
            if (!ok) { ViewBag.Error = "Kullanıcı adı veya şifre hatalı."; return View(); }
            HttpContext.Session.SetString("AdminLoggedIn", "true");
            return RedirectToAction("Index");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
                return RedirectToAction("Login");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Stats()
        {
            if (HttpContext.Session.GetString("AdminLoggedIn") != "true")
                return Unauthorized();

            var activeSeason = await _api.GetActiveSeasonAsync();
            var matches = await _api.GetMatchesAsync(activeSeason?.Id);

            var recent = matches
                .Where(m => m.Status == "Finished")
                .OrderByDescending(m => m.MatchDate)
                .Take(5)
                .Select(m => new
                {
                    m.Id,
                    m.HomeTeam,
                    m.AwayTeam,
                    m.HomeTeamLogo,
                    m.AwayTeamLogo,
                    m.HomeScoreFT,
                    m.AwayScoreFT,
                    m.Status
                })
                .ToList();

            return Json(new
            {
                total = matches.Count,
                live = matches.Count(m => m.Status == "InProgress"),
                finished = matches.Count(m => m.Status == "Finished"),
                pending = matches.Count(m => m.Status == "NotPlayed"),
                recent
            });
        }
    }
}