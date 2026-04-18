namespace GoalZone.API.DTOs.MatchDtos
{
    public class UpdateScoreRequest
    {
        public int? HomeScoreHT { get; set; }
        public int? AwayScoreHT { get; set; }
        public int? HomeScoreFT { get; set; }
        public int? AwayScoreFT { get; set; }
        public string Status { get; set; } = string.Empty;
        public int? CurrentMinute { get; set; }
    }
}