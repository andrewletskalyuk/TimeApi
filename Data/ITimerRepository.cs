namespace TimeApi.Data;

public interface ITimerRepository
{
    void Add(Models.Timer  timer);
    Models.Timer Get(string id);
    Task Update(Models.Timer timer);
    List<Models.Timer> GetAll();

}
