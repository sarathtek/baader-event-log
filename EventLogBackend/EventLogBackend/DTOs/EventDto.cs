namespace EventLogBackend.DTOs;

public class EventDto
{
    public DateTime Timestamp { get; set; }
    public string Severity { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
}
