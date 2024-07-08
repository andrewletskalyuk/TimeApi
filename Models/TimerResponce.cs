namespace TimeApi.Models;

public class TimerResponce
{
    public string Id { get; set; } = "";
    public DateTime DateCreated { get; set; }
    public int Hours { get; set; }
    public int Minutes { get; set; }
    public int Seconds { get; set; }
    public int TimerLeft { get; set; }
    public string WebHookUrl { get; set; } = "";
    public string Status { get; set; } = "";
}
