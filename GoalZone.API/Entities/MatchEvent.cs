using GoalZone.API.Enums;

namespace GoalZone.API.Entities
{
    public class MatchEvent
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public Match Match { get; set; } = null!;
        public bool IsHomeTeam { get; set; }
        public MatchEventType EventType { get; set; }
        //public string EventType { get; set; }
        public int Minute { get; set; }
        public string? Description { get; set; }
        public string? PlayerName { get; set; }
        public string? SubstitutedPlayerName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
