namespace GoalZone.API.DTOs.MatchEventDtos
{
    public class AddMatchEventRequest
    {
        public bool IsHomeTeam { get; set; }
        public string EventType { get; set; } = string.Empty;
        public int Minute { get; set; }
        public string? PlayerName { get; set; }
        public string? SubstitutedPlayerName { get; set; }
        public string? Description { get; set; }
    }
}