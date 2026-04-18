using AutoMapper;
using GoalZone.API.Data;
using GoalZone.API.DTOs.TeamDtos;
using GoalZone.API.Repositories.InterFaces;
using GoalZone.API.Services.TeamServices;

namespace GoalZone.API.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _repo;
        private readonly IMapper _mapper;
        private readonly GoalZoneDbContext _context;

        public TeamService(ITeamRepository repo, IMapper mapper, GoalZoneDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<TeamDto>> GetAllAsync()
        {
            var teams = await _repo.GetAllWithCityAsync();
            return _mapper.Map<IEnumerable<TeamDto>>(teams);
        }

        public async Task ToggleActiveAsync(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team == null) return;
            team.IsActive = !team.IsActive;
            await _context.SaveChangesAsync();
        }
    }

}