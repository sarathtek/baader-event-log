namespace EventLogBackend.Models;

public class Event
{
    public DateTime Timestamp { get; set; }
    public Severity Severity { get; set; }
    public string Message { get; set; } = string.Empty;
}

public enum Severity
{
    Low,
    Medium,
    High
}