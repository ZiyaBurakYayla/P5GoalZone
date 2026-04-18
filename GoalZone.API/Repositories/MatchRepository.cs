using GoalZone.API.Data;
using GoalZone.API.Entities;
using GoalZone.API.Enums;
using GoalZone.API.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace GoalZone.API.Repositories
{
    public class MatchRepository : Repository<Match>, IMatchRepository
    {
        public MatchRepository(GoalZoneDbContext context) : base(context) { }

        public async Task<IEnumerable<Match>> GetMatchesBySeasonAsync(int seasonId) =>
            await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Season)
                .Include(m => m.Referee)
                .Where(m => m.SeasonId == seasonId)
                .OrderBy(m => m.WeekNumber).ThenBy(m => m.MatchDate)
                .ToListAsync();

        public async Task<IEnumerable<Match>> GetMatchesByWeekAsync(int seasonId, int weekNumber) =>
            await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Season)
                .Include(m => m.Referee)
                .Where(m => m.SeasonId == seasonId && m.WeekNumber == weekNumber)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

        public async Task<IEnumerable<Match>> GetMatchesByStatusAsync(MatchStatus status) =>
            await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.Season)
                .Include(m => m.Referee)
                .Where(m => m.Status == status)
                .OrderBy(m => m.MatchDate)
                .ToListAsync();

        public async Task<Match?> GetMatchWithDetailsAsync(int matchId) =>
            await _context.Matches
                .Include(m => m.HomeTeam).ThenInclude(t => t.City)
                .Include(m => m.AwayTeam).ThenInclude(t => t.City)
                .Include(m => m.Season)
                .Include(m => m.Referee)
                .Include(m => m.MatchEvents.OrderBy(e => e.Minute))
                .Include(m => m.MatchStatistic)
                .FirstOrDefaultAsync(m => m.Id == matchId);

        public async Task<IEnumerable<Match>> GetFinishedMatchesBySeasonAsync(int seasonId) =>
            await _context.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Where(m => m.SeasonId == seasonId && m.Status == MatchStatus.Finished)
                .ToListAsync();
    }
}