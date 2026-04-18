using AutoMapper;
using GoalZone.API.DTOs.RefereeDtos;
using GoalZone.API.Repositories.InterFaces;
using GoalZone.API.Services.RefereeServices;

namespace GoalZone.API.Services
{
    public class RefereeService : IRefereeService
    {
        private readonly IRefereeRepository _repo;
        private readonly IMapper _mapper;
        public RefereeService(IRefereeRepository repo, IMapper mapper) { _repo = repo; _mapper = mapper; }

        public async Task<IEnumerable<RefereeDto>> GetAllAsync()
        {
            var referees = await _repo.GetAllAsync();
            return _mapper.Map<IEnumerable<RefereeDto>>(referees);
        }
    }
}