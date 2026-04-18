namespace GoalZone.API.DTOs.SeasonDtos
{
    public class CreateSeasonRequest
    {
        public string Name { get; set; } = string.Empty;
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public bool IsActive { get; set; }
    }
}
