namespace GoalZone.API.Entities
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Country { get; set; } = "England";
        public ICollection<Team> Teams { get; set; } = new List<Team>();
    }
}
