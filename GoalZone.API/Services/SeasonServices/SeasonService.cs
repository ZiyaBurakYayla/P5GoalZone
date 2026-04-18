using AutoMapper;
using GoalZone.API.Data;
using GoalZone.API.DTOs.SeasonDtos;
using GoalZone.API.Entities;
using GoalZone.API.Repositories;
using GoalZone.API.Services.SeasonServices;
using Microsoft.EntityFrameworkCore;

public class SeasonService : ISeasonService
{
    private readonly ISeasonRepository _repo;
    private readonly IMapper _mapper;
    private readonly GoalZoneDbContext _context;

    public SeasonService(ISeasonRepository repo, IMapper mapper, GoalZoneDbContext context)
    {
        _repo = repo;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<SeasonDto>> GetAllAsync()
    {
        var seasons = await _repo.GetAllAsync();
        return _mapper.Map<IEnumerable<SeasonDto>>(seasons.OrderByDescending(s => s.StartYear));
    }

    public async Task<SeasonDto?> GetActiveAsync()
    {
        var season = await _repo.GetActiveSeasonAsync();
        return season == null ? null : _mapper.Map<SeasonDto>(season);
    }

    public async Task<SeasonDto?> GetByIdAsync(int id)
    {
        var season = await _repo.GetByIdAsync(id);
        return season == null ? null : _mapper.Map<SeasonDto>(season);
    }

    public async Task SetActiveSeasonAsync(int seasonId)
    {
        var all = await _context.Seasons.ToListAsync();
        foreach (var s in all)
            s.IsActive = s.Id == seasonId;
        await _context.SaveChangesAsync();
    }

    public async Task<SeasonDto> CreateSeasonAsync(string name, int startYear, int endYear, bool setActive)
    {
        if (setActive)
        {
            var all = await _context.Seasons.ToListAsync();
            foreach (var s in all) s.IsActive = false;
        }

        var season = new Season
        {
            Name = name,
            StartYear = startYear,
            EndYear = endYear,
            IsActive = setActive
        };
        await _context.Seasons.AddAsync(season);
        await _context.SaveChangesAsync();
        return _mapper.Map<SeasonDto>(season);
    }
}