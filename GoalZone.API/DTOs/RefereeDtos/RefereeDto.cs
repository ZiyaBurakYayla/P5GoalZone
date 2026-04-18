namespace GoalZone.API.DTOs.RefereeDtos
{
    public class RefereeDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Nationality { get; set; }
    }
}