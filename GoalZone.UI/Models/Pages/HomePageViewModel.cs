using GoalZone.UI.Models.Components;
using System.Collections.Generic;

namespace GoalZone.UI.Models.Pages
{
    public class HomePageViewModel
    {
        public List<MatchListViewModel> LiveMatches { get; set; } = new();
        public List<MatchListViewModel> RecentMatches { get; set; } = new();
        public List<MatchListViewModel> UpcomingMatches { get; set; } = new();
        public MatchListViewModel? FeaturedMatch { get; set; }
        public SeasonViewModel? ActiveSeason { get; set; }
        public List<SeasonViewModel> AllSeasons { get; set; } = new();
    }
}