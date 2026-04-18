using AutoMapper;
using GoalZone.API.DTOs.CityDtos;
using GoalZone.API.DTOs.MatchDtos;
using GoalZone.API.DTOs.MatchEventDtos;
using GoalZone.API.DTOs.RefereeDtos;
using GoalZone.API.DTOs.SeasonDtos;
using GoalZone.API.DTOs.TeamDtos;
using GoalZone.API.Entities;

namespace GoalZone.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<City, CityDto>();

            CreateMap<Season, SeasonDto>();

            CreateMap<Referee, RefereeDto>();

            CreateMap<Team, TeamDto>()
                .ForMember(d => d.CityName, o => o.MapFrom(s => s.City != null ? s.City.Name : null));

            CreateMap<Match, MatchListDto>()
                .ForMember(d => d.SeasonName,    o => o.MapFrom(s => s.Season.Name))
                .ForMember(d => d.HomeTeam,      o => o.MapFrom(s => s.HomeTeam.Name))
                .ForMember(d => d.HomeTeamShort, o => o.MapFrom(s => s.HomeTeam.ShortCode))
                .ForMember(d => d.HomeTeamLogo,  o => o.MapFrom(s => s.HomeTeam.LogoUrl))
                .ForMember(d => d.AwayTeam,      o => o.MapFrom(s => s.AwayTeam.Name))
                .ForMember(d => d.AwayTeamShort, o => o.MapFrom(s => s.AwayTeam.ShortCode))
                .ForMember(d => d.AwayTeamLogo,  o => o.MapFrom(s => s.AwayTeam.LogoUrl))
                .ForMember(d => d.RefereeName,   o => o.MapFrom(s => s.Referee != null ? s.Referee.FullName : null))
                .ForMember(d => d.Status,        o => o.MapFrom(s => s.Status.ToString()));

            CreateMap<Match, MatchDetailDto>()
                .ForMember(d => d.SeasonName,    o => o.MapFrom(s => s.Season.Name))
                .ForMember(d => d.HomeTeam,      o => o.MapFrom(s => s.HomeTeam.Name))
                .ForMember(d => d.HomeTeamLogo,  o => o.MapFrom(s => s.HomeTeam.LogoUrl))
                .ForMember(d => d.HomeTeamId,    o => o.MapFrom(s => s.HomeTeamId))
                .ForMember(d => d.AwayTeam,      o => o.MapFrom(s => s.AwayTeam.Name))
                .ForMember(d => d.AwayTeamLogo,  o => o.MapFrom(s => s.AwayTeam.LogoUrl))
                .ForMember(d => d.AwayTeamId,    o => o.MapFrom(s => s.AwayTeamId))
                .ForMember(d => d.RefereeName,   o => o.MapFrom(s => s.Referee != null ? s.Referee.FullName : null))
                .ForMember(d => d.Status,        o => o.MapFrom(s => s.Status.ToString()))
                .ForMember(d => d.Events,        o => o.MapFrom(s => s.MatchEvents.OrderBy(e => e.Minute)))
                .ForMember(d => d.Statistics,    o => o.MapFrom(s => s.MatchStatistic));

            CreateMap<MatchEvent, MatchEventDto>()
                .ForMember(d => d.EventType, o => o.MapFrom(s => s.EventType.ToString()));

            CreateMap<MatchStatistic, MatchStatisticDto>();
        }
    }
}
