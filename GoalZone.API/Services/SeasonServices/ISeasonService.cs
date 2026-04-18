using GoalZone.API.DTOs.SeasonDtos;

namespace GoalZone.API.Services.SeasonServices
{
    public interface ISeasonService
    {
        Task<IEnumerable<SeasonDto>> GetAllAsync();
        Task<SeasonDto?> GetActiveAsync();
        Task<SeasonDto?> GetByIdAsync(int id);
        Task SetActiveSeasonAsync(int seasonId);
        Task<SeasonDto> CreateSeasonAsync(string name, int startYear, int endYear, bool setActive);
    }
}
