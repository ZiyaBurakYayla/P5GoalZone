namespace GoalZone.API.DTOs.MatchDtos
{
    public class MatchStatisticDto
    {
        public string? HalfTimeScore { get; set; }
        public string? SecondHalfScore { get; set; }
        public decimal? HomePossession { get; set; }
        public decimal? AwayPossession { get; set; }
        public int? HomeTotalShots { get; set; }
        public int? AwayTotalShots { get; set; }
        public int? HomeShotsOnTarget { get; set; }
        public int? AwayShotsOnTarget { get; set; }
        public int? HomePasses { get; set; }
        public int? AwayPasses { get; set; }
        public decimal? HomePassAccuracy { get; set; }
        public decimal? AwayPassAccuracy { get; set; }
        public int? HomeCorners { get; set; }
        public int? AwayCorners { get; set; }
        public int? HomeFouls { get; set; }
        public int? AwayFouls { get; set; }
        public int? HomeOffsides { get; set; }
        public int? AwayOffsides { get; set; }
        public int? HomeYellowCards { get; set; }
        public int? AwayYellowCards { get; set; }
        public int? HomeRedCards { get; set; }
        public int? AwayRedCards { get; set; }
    }
}