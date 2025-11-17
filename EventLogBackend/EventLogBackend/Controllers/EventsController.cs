using EventLogBackend.Repository;
using EventLogBackend.DTOs;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace EventLogBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EventsController : ControllerBase
{
    private readonly IEventRepository _repository;
    private readonly IMapper _mapper;

    public EventsController(IEventRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    [HttpGet]
    public ActionResult<IEnumerable<EventDto>> GetEvents([FromQuery] DateTime startTime)
    {
        var events = _repository.GetEventsSince(startTime);
        var eventDtos = _mapper.Map<List<EventDto>>(events);
        return Ok(eventDtos);
    }
}
