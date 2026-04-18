namespace GoalZone.API.Entities
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortCode { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? Stadium { get; set; }
        public int? StadiumCapacity { get; set; }
        public int? FoundedYear { get; set; }
        public int? CityId { get; set; }
        public City? City { get; set; }
        public bool IsActive { get; set; } = true;
        public ICollection<Match> HomeMatches { get; set; } = new List<Match>();
        public ICollection<Match> AwayMatches { get; set; } = new List<Match>();
    }
}
