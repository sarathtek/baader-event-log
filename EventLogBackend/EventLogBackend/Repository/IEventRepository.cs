using EventLogBackend.Models;

namespace EventLogBackend.Repository;

public interface IEventRepository
{
    void Add(Event entry);
    IReadOnlyList<Event> GetEventsSince(DateTime startTime);
}
