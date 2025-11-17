using EventLogBackend.Models;
using EventLogBackend.Repository;

namespace EventLogBackend.Services;

// Event generator service that simulates matchine events.
public class EventGeneratorService : BackgroundService
{
    private readonly IEventRepository _eventRepository;
    private readonly ILogger<EventGeneratorService> _logger;
    private readonly Random _random = new();

    private readonly string[] SampleMessages =
    {
        "Conveyor belt speed adjusted by operator",
        "User login detected from IP {0}",
        "Database query executed",
        "Safety guard opened – machine paused automatically",
        "New firmware version available for download",
        "API request processed",
        "File uploaded to storage",
        "Predictive maintenance model flagged bearing wear risk",
        "Background job started",
        "Email notification sent",
        "Network latency detected",
        "Health check performed",
        "Machine A running normally",
        "Temperature spike detected",
        "Vibration warning",
        "Line 2 throughput low",
        "Maintenance check recommended",
        "Sensor drift detected"
    };

    public EventGeneratorService(IEventRepository eventRepository, ILogger<EventGeneratorService> logger)
    {
        _eventRepository = eventRepository;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("Event Generator Service started");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                var entry = new Event
                {
                    Timestamp = DateTime.UtcNow,
                    Severity = GetRandomSeverity(),
                    Message = GetRandomSampleMessage()
                };

                _eventRepository.Add(entry);

                await Task.Delay(_random.Next(500, 2000), stoppingToken);
            }
            catch (OperationCanceledException)
            {
                break;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error Generating Event");
                await Task.Delay(1000, stoppingToken);
            }
        }

        _logger.LogInformation("Event Generator Service stopping");
    }

    private Severity GetRandomSeverity()
    {
        var values = Enum.GetValues<Severity>();
        return values[_random.Next(values.Length)];
    }

    private string GetRandomSampleMessage()
    {
        return SampleMessages[_random.Next(SampleMessages.Length)];
    }
}
