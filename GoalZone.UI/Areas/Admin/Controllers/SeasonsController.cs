using GoalZone.WebUI.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoalZone.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SeasonsController : Controller
    {
        private readonly ApiClient _api;
        public SeasonsController(ApiClient api) => _api = api;

        private bool IsLoggedIn => HttpContext.Session.GetString("AdminLoggedIn") == "true";

        public async Task<IActionResult> Index()
        {
            if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
            var seasons = await _api.GetSeasonsAsync();
            return View(seasons);
        }

        [HttpPost]
        public async Task<IActionResult> Activate(int seasonId)
        {
            if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
            var ok = await _api.ActivateSeasonAsync(seasonId);
            if (ok)
                TempData["Success"] = "Aktif sezon başarıyla güncellendi.";
            else
                TempData["Error"] = "Sezon güncellenirken bir hata oluştu.";
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Create(string name, int startYear, int endYear, bool setActive)
        {
            if (!IsLoggedIn) return RedirectToAction("Login", "Dashboard");
            var ok = await _api.CreateSeasonAsync(name, startYear, endYear, setActive);
            if (ok)
                TempData["Success"] = $"{name} sezonu başarıyla oluşturuldu.";
            else
                TempData["Error"] = "Sezon oluşturulurken bir hata oluştu.";
            return RedirectToAction("Index");
        }
    }
}
