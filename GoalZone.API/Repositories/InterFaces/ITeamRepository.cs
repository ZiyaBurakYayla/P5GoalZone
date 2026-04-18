using GoalZone.API.Entities;

namespace GoalZone.API.Repositories.InterFaces
{
    public interface ITeamRepository : IRepository<Team>
    {
        Task<IEnumerable<Team>> GetAllWithCityAsync();
    }
}