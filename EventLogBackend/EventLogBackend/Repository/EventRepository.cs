using System.Collections.Concurrent;
using EventLogBackend.Models;

namespace EventLogBackend.Repository;

// Repository for in-memory event storage and retrieval.
public class EventRepository : IEventRepository
{
    private readonly ConcurrentQueue<Event> _events = new();
    private const int MaxEvents = 2000;

    public void Add(Event entry)
    {
        _events.Enqueue(entry);

        while (_events.Count > MaxEvents && _events.TryDequeue(out _)) { }
    }

    public IReadOnlyList<Event> GetEventsSince(DateTime startTime)
    {
        return _events
            .Where(e => e.Timestamp >= startTime)
            .OrderBy(e => e.Timestamp)
            .ToList();
    }
}
