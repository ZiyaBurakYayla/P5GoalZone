namespace GoalZone.API.DTOs.TeamDtos
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortCode { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? Stadium { get; set; }
        public int? StadiumCapacity { get; set; }
        public int? FoundedYear { get; set; }
        public string? CityName { get; set; }
        public bool IsActive { get; set; }
    }
}