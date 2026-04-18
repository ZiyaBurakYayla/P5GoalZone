using GoalZone.UI.Models.Components;

namespace GoalZone.UI.Models.Pages
{
    public class StandingsPageViewModel
    {
        public List<StandingViewModel> Standings { get; set; } = new();
        public SeasonViewModel? ActiveSeason { get; set; }
        public List<SeasonViewModel> AllSeasons { get; set; } = new();
        public int SelectedSeasonId { get; set; }
    }
}
