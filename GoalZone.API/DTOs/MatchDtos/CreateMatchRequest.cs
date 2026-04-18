namespace GoalZone.API.DTOs.MatchDtos
{
    public class CreateMatchRequest
    {
        public int SeasonId { get; set; }
        public int WeekNumber { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime MatchDate { get; set; }
        public string? Venue { get; set; }
        public int? RefereeId { get; set; }
        public bool IsFeatured { get; set; }
    }
}