using GoalZone.API.Data;
using GoalZone.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace GoalZone.API.Repositories
{
    public class SeasonRepository : Repository<Season>, ISeasonRepository
    {
        public SeasonRepository(GoalZoneDbContext context) : base(context) { }

        public async Task<Season?> GetActiveSeasonAsync() =>
            await _context.Seasons.FirstOrDefaultAsync(s => s.IsActive);
    }
}