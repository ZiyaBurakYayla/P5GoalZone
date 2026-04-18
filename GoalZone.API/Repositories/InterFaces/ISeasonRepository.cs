using GoalZone.API.Entities;
using GoalZone.API.Repositories.InterFaces;

namespace GoalZone.API.Repositories
{
    public interface ISeasonRepository : IRepository<Season>
    {
        Task<Season?> GetActiveSeasonAsync();
    }
}