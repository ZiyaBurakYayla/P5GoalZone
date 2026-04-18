namespace GoalZone.API.Entities
{
    public class Referee
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Nationality { get; set; }
        public ICollection<Match> Matches { get; set; } = new List<Match>();
    }
}
