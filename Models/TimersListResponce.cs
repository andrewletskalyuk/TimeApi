namespace TimeApi.Models;

public class TimersListResponce
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public List<TimerResponce> Items { get; set; }
    public int TotalRowCount { get; set; }
}
