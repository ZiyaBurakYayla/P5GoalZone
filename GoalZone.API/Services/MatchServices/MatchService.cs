using AutoMapper;
using GoalZone.API.Data;
using GoalZone.API.DTOs.MatchDtos;
using GoalZone.API.DTOs.MatchEventDtos;
using GoalZone.API.Entities;
using GoalZone.API.Enums;
using GoalZone.API.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace GoalZone.API.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepo;
        private readonly GoalZoneDbContext _context;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository matchRepo, GoalZoneDbContext context, IMapper mapper)
        {
            _matchRepo = matchRepo;
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MatchListDto>> GetAllMatchesAsync(int seasonId)
        {
            var matches = await _matchRepo.GetMatchesBySeasonAsync(seasonId);
            return _mapper.Map<IEnumerable<MatchListDto>>(matches);
        }

        public async Task<IEnumerable<MatchListDto>> GetLiveMatchesAsync()
        {
            var matches = await _matchRepo.GetMatchesByStatusAsync(MatchStatus.InProgress);
            return _mapper.Map<IEnumerable<MatchListDto>>(matches);
        }

        public async Task<IEnumerable<MatchListDto>> GetMatchesByWeekAsync(int seasonId, int week)
        {
            var matches = await _matchRepo.GetMatchesByWeekAsync(seasonId, week);
            return _mapper.Map<IEnumerable<MatchListDto>>(matches);
        }

        public async Task<MatchDetailDto?> GetMatchDetailAsync(int matchId)
        {
            var match = await _matchRepo.GetMatchWithDetailsAsync(matchId);
            return match == null ? null : _mapper.Map<MatchDetailDto>(match);
        }

        public async Task<int> CreateMatchAsync(CreateMatchRequest req)
        {
            var match = new Match
            {
                SeasonId = req.SeasonId,
                WeekNumber = req.WeekNumber,
                HomeTeamId = req.HomeTeamId,
                AwayTeamId = req.AwayTeamId,
                MatchDate = req.MatchDate,
                Venue = req.Venue,
                RefereeId = req.RefereeId,
                IsFeatured = req.IsFeatured,
                Status = MatchStatus.NotPlayed
            };
            await _matchRepo.AddAsync(match);
            await _matchRepo.SaveChangesAsync();
            return match.Id;
        }

        public async Task UpdateScoreAsync(int matchId, UpdateScoreRequest req)
        {
            var match = await _matchRepo.GetByIdAsync(matchId)
                        ?? throw new KeyNotFoundException($"Match {matchId} not found.");
            match.HomeScoreHT = req.HomeScoreHT;
            match.AwayScoreHT = req.AwayScoreHT;
            match.HomeScoreFT = req.HomeScoreFT;
            match.AwayScoreFT = req.AwayScoreFT;
            match.CurrentMinute = req.CurrentMinute;
            if (Enum.TryParse<MatchStatus>(req.Status, out var status))
                match.Status = status;
            _matchRepo.Update(match);
            await _matchRepo.SaveChangesAsync();
        }

        public async Task AddMatchEventAsync(int matchId, AddMatchEventRequest req)
        {
            if (!Enum.TryParse<MatchEventType>(req.EventType, out var eventType))
                throw new ArgumentException($"Invalid EventType: {req.EventType}");
            var ev = new MatchEvent
            {
                MatchId = matchId,
                IsHomeTeam = req.IsHomeTeam,
                EventType = eventType,
                Minute = req.Minute,
                PlayerName = req.PlayerName,
                SubstitutedPlayerName = req.SubstitutedPlayerName,
                Description = req.Description
            };
            await _context.MatchEvents.AddAsync(ev);
            await _context.SaveChangesAsync();
        }

        public async Task UpsertStatisticAsync(int matchId, UpsertStatisticRequest req)
        {
            var stat = await _context.MatchStatistics.FirstOrDefaultAsync(s => s.MatchId == matchId);
            if (stat == null)
            {
                stat = new MatchStatistic { MatchId = matchId };
                await _context.MatchStatistics.AddAsync(stat);
            }
            stat.HalfTimeScore = req.HalfTimeScore;
            stat.SecondHalfScore = req.SecondHalfScore;
            stat.HomePossession = req.HomePossession;
            stat.AwayPossession = req.AwayPossession;
            stat.HomeTotalShots = req.HomeTotalShots;
            stat.AwayTotalShots = req.AwayTotalShots;
            stat.HomeShotsOnTarget = req.HomeShotsOnTarget;
            stat.AwayShotsOnTarget = req.AwayShotsOnTarget;
            stat.HomePasses = req.HomePasses;
            stat.AwayPasses = req.AwayPasses;
            stat.HomePassAccuracy = req.HomePassAccuracy;
            stat.AwayPassAccuracy = req.AwayPassAccuracy;
            stat.HomeCorners = req.HomeCorners;
            stat.AwayCorners = req.AwayCorners;
            stat.HomeFouls = req.HomeFouls;
            stat.AwayFouls = req.AwayFouls;
            stat.HomeOffsides = req.HomeOffsides;
            stat.AwayOffsides = req.AwayOffsides;
            stat.HomeYellowCards = req.HomeYellowCards;
            stat.AwayYellowCards = req.AwayYellowCards;
            stat.HomeRedCards = req.HomeRedCards;
            stat.AwayRedCards = req.AwayRedCards;
            stat.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMatchEventAsync(int eventId)
        {
            var ev = await _context.MatchEvents.FindAsync(eventId)
                     ?? throw new KeyNotFoundException($"Event {eventId} not found.");
            _context.MatchEvents.Remove(ev);
            await _context.SaveChangesAsync();
        }
    }
}