using TimeApi.Models;

namespace TimeApi.Services;

public interface ITimerService
{
    TimerResponce SetTimer(SetTimerRequest  request);
    TimerStatusResponce GetTimerStatus(string id);
    TimersListResponce ListTimers(int pageNumber,  int pageSize);
}
