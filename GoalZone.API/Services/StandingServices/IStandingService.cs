using GoalZone.API.DTOs.StandingDtos;

namespace GoalZone.API.Services.StandingServices
{
    public interface IStandingService
    {
        Task<IEnumerable<StandingDto>> GetStandingsAsync(int seasonId);
    }
}
