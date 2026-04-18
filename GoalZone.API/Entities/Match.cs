using GoalZone.API.Enums;

namespace GoalZone.API.Entities
{
    public class Match
    {
        public int Id { get; set; }
        public int SeasonId { get; set; }
        public Season Season { get; set; } = null!;
        public int WeekNumber { get; set; }
        public int HomeTeamId { get; set; }
        public Team HomeTeam { get; set; } = null!;
        public int AwayTeamId { get; set; }
        public Team AwayTeam { get; set; } = null!;
        public DateTime MatchDate { get; set; }
        public string? Venue { get; set; }
        public int? RefereeId { get; set; }
        public Referee? Referee { get; set; }
        public int? Attendance { get; set; }
        public int? HomeScoreHT { get; set; }
        public int? AwayScoreHT { get; set; }
        public int? HomeScoreFT { get; set; }
        public int? AwayScoreFT { get; set; }
        public MatchStatus Status { get; set; } = MatchStatus.NotPlayed;
        public int? CurrentMinute { get; set; }
        public bool IsFeatured { get; set; }
        public ICollection<MatchEvent> MatchEvents { get; set; } = new List<MatchEvent>();
        public MatchStatistic? MatchStatistic { get; set; }
    }
}
