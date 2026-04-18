using System;

namespace GoalZone.UI.Models.Components
{
    public class MatchListViewModel
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public string SeasonName { get; set; } = string.Empty;
        public string HomeTeam { get; set; } = string.Empty;
        public string HomeTeamShort { get; set; } = string.Empty;
        public string? HomeTeamLogo { get; set; }
        public string AwayTeam { get; set; } = string.Empty;
        public string AwayTeamShort { get; set; } = string.Empty;
        public string? AwayTeamLogo { get; set; }
        public DateTime MatchDate { get; set; }
        public string? Venue { get; set; }
        public string? RefereeName { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? CurrentMinute { get; set; }
        public int? HomeScoreFT { get; set; }
        public int? AwayScoreFT { get; set; }
        public bool IsFeatured { get; set; }
    }
}