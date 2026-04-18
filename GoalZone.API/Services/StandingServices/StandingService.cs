using GoalZone.API.Data;
using GoalZone.API.DTOs.StandingDtos;
using GoalZone.API.Repositories.InterFaces;
using GoalZone.API.Services.StandingServices;
using Microsoft.EntityFrameworkCore;

namespace GoalZone.API.Services
{
    public class StandingService : IStandingService
    {
        private readonly IMatchRepository _matchRepo;
        private readonly GoalZoneDbContext _context;

        public StandingService(IMatchRepository matchRepo, GoalZoneDbContext context)
        {
            _matchRepo = matchRepo;
            _context = context;
        }

        public async Task<IEnumerable<StandingDto>> GetStandingsAsync(int seasonId)
        {
            var matches = await _matchRepo.GetFinishedMatchesBySeasonAsync(seasonId);

            var activeTeamIds = (await _context.Teams
                .Where(t => t.IsActive)
                .Select(t => t.Id)
                .ToListAsync())
                .ToHashSet();

            var table = new Dictionary<int, StandingDto>();

            foreach (var match in matches)
            {
                if (match.HomeScoreFT == null || match.AwayScoreFT == null) continue;
                if (!activeTeamIds.Contains(match.HomeTeamId) || !activeTeamIds.Contains(match.AwayTeamId)) continue;

                int hG = match.HomeScoreFT.Value, aG = match.AwayScoreFT.Value;

                if (!table.ContainsKey(match.HomeTeamId))
                    table[match.HomeTeamId] = new StandingDto { TeamId = match.HomeTeamId, TeamName = match.HomeTeam.Name, ShortCode = match.HomeTeam.ShortCode, LogoUrl = match.HomeTeam.LogoUrl };
                if (!table.ContainsKey(match.AwayTeamId))
                    table[match.AwayTeamId] = new StandingDto { TeamId = match.AwayTeamId, TeamName = match.AwayTeam.Name, ShortCode = match.AwayTeam.ShortCode, LogoUrl = match.AwayTeam.LogoUrl };

                var home = table[match.HomeTeamId];
                var away = table[match.AwayTeamId];

                home.Played++; away.Played++;
                home.GoalsFor += hG; home.GoalsAgainst += aG;
                away.GoalsFor += aG; away.GoalsAgainst += hG;

                if (hG > aG) { home.Won++; home.Last5.Add("W"); away.Lost++; away.Last5.Add("L"); }
                else if (hG == aG) { home.Drawn++; home.Last5.Add("D"); away.Drawn++; away.Last5.Add("D"); }
                else { home.Lost++; home.Last5.Add("L"); away.Won++; away.Last5.Add("W"); }
            }

            foreach (var e in table.Values)
                if (e.Last5.Count > 5) e.Last5 = e.Last5.TakeLast(5).ToList();

            var ranked = table.Values
                .OrderByDescending(t => t.Points)
                .ThenByDescending(t => t.GoalDifference)
                .ThenByDescending(t => t.GoalsFor)
                .ToList();

            for (int i = 0; i < ranked.Count; i++) ranked[i].Rank = i + 1;
            return ranked;
        }
    }
}
