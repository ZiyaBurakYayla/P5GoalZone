namespace GoalZone.UI.Models.Components
{
    public class MatchEventViewModel
    {
        public int Id { get; set; }
        public string EventType { get; set; } = string.Empty;
        public bool IsHomeTeam { get; set; }
        public int Minute { get; set; }
        public string? PlayerName { get; set; }
        public string? SubstitutedPlayerName { get; set; }
        public string? Description { get; set; }
    }
}