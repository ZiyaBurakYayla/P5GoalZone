using GoalZone.API.DTOs.TeamDtos;

namespace GoalZone.API.Services.TeamServices
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamDto>> GetAllAsync();
        Task ToggleActiveAsync(int teamId);
    }
}
