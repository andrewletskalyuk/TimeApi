namespace TimeApi.Data;

public class TimerRepository : ITimerRepository
{
    //TODO we need a real rep
    readonly Dictionary<string, Models.Timer> _timers = new();
    public void Add(Models.Timer timer)
    {
        _timers[timer.Id] = timer;
    }

    public Models.Timer Get(string id)
    {
        _timers.TryGetValue(id, out var timer);
        return timer;
    }

    public List<Models.Timer> GetAll()
    {
        return new List<Models.Timer>(_timers.Values);
    }

    public Task Update(Models.Timer timer)
    {
        _timers[timer.Id] = timer;
        return Task.CompletedTask;
    }
}
