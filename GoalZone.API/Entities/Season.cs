namespace GoalZone.API.Entities
{
    public class Season
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int StartYear { get; set; }
        public int EndYear { get; set; }
        public bool IsActive { get; set; }
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
