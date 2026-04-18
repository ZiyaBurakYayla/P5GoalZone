using GoalZone.API.Data;
using GoalZone.API.Entities;
using GoalZone.API.Repositories.InterFaces;

namespace GoalZone.API.Repositories
{
    public class RefereeRepository : Repository<Referee>, IRefereeRepository
    {
        public RefereeRepository(GoalZoneDbContext context) : base(context) { }
    }
}