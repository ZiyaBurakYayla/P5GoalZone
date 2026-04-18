using GoalZone.UI.Models.Components;
using System.Collections.Generic;
using System.Linq;

namespace GoalZone.UI.Models.Pages
{
    public class FixturePageViewModel
    {
        public List<IGrouping<int, MatchListViewModel>> MatchesByWeek { get; set; } = new();
        public int CurrentWeek { get; set; }
        public SeasonViewModel? ActiveSeason { get; set; }
        public List<SeasonViewModel> AllSeasons { get; set; } = new();
        public int SelectedSeasonId { get; set; }
    }
}