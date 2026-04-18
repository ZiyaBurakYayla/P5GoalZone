using GoalZone.API.Data;
using GoalZone.API.Entities;
using GoalZone.API.Repositories.InterFaces;

namespace GoalZone.API.Repositories
{
    public class CityRepository : Repository<City>, ICityRepository
    {
        public CityRepository(GoalZoneDbContext context) : base(context) { }
    }
}