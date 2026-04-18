using GoalZone.API.DTOs.MatchEventDtos;

namespace GoalZone.API.DTOs.MatchDtos
{
    public class MatchDetailDto
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public string SeasonName { get; set; } = string.Empty;
        public string HomeTeam { get; set; } = string.Empty;
        public string AwayTeam { get; set; } = string.Empty;
        public string? HomeTeamLogo { get; set; }
        public string? AwayTeamLogo { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public string? Venue { get; set; }
        public string? RefereeName { get; set; }
        public int? Attendance { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? CurrentMinute { get; set; }
        public int? HomeScoreHT { get; set; }
        public int? AwayScoreHT { get; set; }
        public int? HomeScoreFT { get; set; }
        public int? AwayScoreFT { get; set; }
        public bool IsFeatured { get; set; }
        public List<MatchEventDto> Events { get; set; } = new();
        public MatchStatisticDto? Statistics { get; set; }
    }
}