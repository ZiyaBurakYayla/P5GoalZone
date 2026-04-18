using GoalZone.API.DTOs.MatchDtos;
using GoalZone.API.DTOs.MatchEventDtos;

namespace GoalZone.API.Services
{
    public interface IMatchService 
    {
        Task<IEnumerable<MatchListDto>> GetAllMatchesAsync(int seasonId);
        Task<IEnumerable<MatchListDto>> GetLiveMatchesAsync();
        Task<IEnumerable<MatchListDto>> GetMatchesByWeekAsync(int seasonId, int week);
        Task<MatchDetailDto?> GetMatchDetailAsync(int matchId);
        Task<int> CreateMatchAsync(CreateMatchRequest request);
        Task UpdateScoreAsync(int matchId, UpdateScoreRequest request);
        Task AddMatchEventAsync(int matchId, AddMatchEventRequest request);
        Task UpsertStatisticAsync(int matchId, UpsertStatisticRequest request);
        Task DeleteMatchEventAsync(int eventId);
    }
}