namespace TimeApi.Models;

public class Timer
{
    public string Id { get; set; } = "";
    public TimeSpan Duration { get; set; }
    public DateTime EndTime { get; set; }
    public string WebhookUrl { get; set; } = "";
    public bool IsCompleted { get; set; }

}
