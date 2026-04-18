namespace GoalZone.UI.Models.Components
{
    public class SeasonViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public bool IsActive { get; set; }
    }
}