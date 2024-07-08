using TimeApi.Data;
using TimeApi.Models;

namespace TimeApi.Services;

public class TimerService : ITimerService
{
    readonly ITimerRepository _timerRepository;
    public TimerService(ITimerRepository timerRepository)
    {
        _timerRepository=timerRepository;
    }

    public TimerResponce SetTimer(SetTimerRequest request)
    {
        var timerId = Guid.NewGuid().ToString();
        var duration = new TimeSpan(request.Hours, request.Minutes, request.Seconds);
        var endTime = DateTime.UtcNow.Add(duration);

        var timer = new Models.Timer
        {
            Id = timerId,
            Duration = duration,
            EndTime = endTime,
            WebhookUrl = request.WebHookUrl,
            IsCompleted = false
        };

        _timerRepository.Add(timer);

        Task.Run(async () =>
        {
            await Task.Delay(duration);
            timer.IsCompleted = true;
            await _timerRepository.Update(timer);
            await SendWebhook(timer.WebhookUrl);
        });

        return new TimerResponce
        {
            Id = timer.Id,
            DateCreated = DateTime.UtcNow,
            Hours = request.Hours,
            Minutes = request.Minutes,
            Seconds = request.Seconds,
            TimerLeft = (int)duration.TotalSeconds,
            WebHookUrl = request.WebHookUrl,
            Status = "Started"
        };
    }

    private async Task SendWebhook(string webhookUrl)
    {
        //TODO: ...need a real url
        using var client = new HttpClient();
        await client.PostAsync(webhookUrl, null);
    }

    public TimerStatusResponce GetTimerStatus(string id)
    {
        var timer = _timerRepository.Get(id);

        if (timer != null)
        {
            var timeLeft = (int)(timer.EndTime - DateTime.UtcNow).TotalSeconds;
            return new TimerStatusResponce
            {
                Id = timer.Id,
                TimerLeft = timeLeft > 0 ? timeLeft : 0
            };
        }
        return new TimerStatusResponce
        {
            TimerLeft = 0,
            Id = id
        };
    }

    public TimersListResponce ListTimers(int pageNumber, int pageSize)
    {
        var timers = _timerRepository.GetAll();
        var paginatedTimers = timers.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();

        return new TimersListResponce
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            Items = paginatedTimers.Select(timer => new TimerResponce
            {
                Id=timer.Id,
                DateCreated = timer.EndTime - timer.Duration,
                Hours = timer.Duration.Hours, 
                Minutes = timer.Duration.Minutes, 
                Seconds = timer.Duration.Seconds,
                TimerLeft = (int)(timer.EndTime - DateTime.UtcNow).TotalSeconds,
                WebHookUrl = timer.WebhookUrl,
                Status = timer.IsCompleted ? "Finished" : "Started"
            }).ToList(),
            TotalRowCount = timers.Count
        };
    }

}
