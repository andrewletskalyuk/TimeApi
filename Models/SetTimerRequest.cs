namespace TimeApi.Models;

public class SetTimerRequest
{
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }
    public string WebHookUrl { get; set; } = "";
}
