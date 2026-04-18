using GoalZone.API.Data;
using GoalZone.API.Entities;
using GoalZone.API.Repositories.InterFaces;
using Microsoft.EntityFrameworkCore;

namespace GoalZone.API.Repositories
{
    public class TeamRepository : Repository<Team>, ITeamRepository
    {
        public TeamRepository(GoalZoneDbContext context) : base(context) { }

        public async Task<IEnumerable<Team>> GetAllWithCityAsync() =>
            await _context.Teams.Include(t => t.City).OrderBy(t => t.Name).ToListAsync();
    }
}