namespace GoalZone.UI.Models.Components
{
    public class TeamViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ShortCode { get; set; } = string.Empty;
        public string? LogoUrl { get; set; }
        public string? Stadium { get; set; }
        public string? CityName { get; set; }
        public bool IsActive { get; set; } = true;
    }
}