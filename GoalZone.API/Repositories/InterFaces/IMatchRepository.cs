using GoalZone.API.Entities;
using GoalZone.API.Enums;

namespace GoalZone.API.Repositories.InterFaces
{
    public interface IMatchRepository : IRepository<Match>
    {
        Task<IEnumerable<Match>> GetMatchesBySeasonAsync(int seasonId);
        Task<IEnumerable<Match>> GetMatchesByWeekAsync(int seasonId, int weekNumber);
        Task<IEnumerable<Match>> GetMatchesByStatusAsync(MatchStatus status);
        Task<Match?> GetMatchWithDetailsAsync(int matchId);
        Task<IEnumerable<Match>> GetFinishedMatchesBySeasonAsync(int seasonId);
    }
}