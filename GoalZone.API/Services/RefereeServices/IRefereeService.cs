using GoalZone.API.DTOs.RefereeDtos;

namespace GoalZone.API.Services.RefereeServices
{
    public interface IRefereeService
    {
        Task<IEnumerable<RefereeDto>> GetAllAsync();
    }
}
