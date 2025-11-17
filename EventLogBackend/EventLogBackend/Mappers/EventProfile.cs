using AutoMapper;
using EventLogBackend.Models;
using EventLogBackend.DTOs;

namespace EventLogBackend.Mappers;

public class EventProfile : Profile
{
    public EventProfile()
    {
        CreateMap<Event, EventDto>()
            .ForMember(dest => dest.Severity, opt => opt.MapFrom(src => src.Severity.ToString()));
    }
}
