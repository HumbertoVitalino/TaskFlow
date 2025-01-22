namespace Core.Interfaces;

public interface ITaskRepository : IBaseRepository<Entities.Task>
{
    Task<Entities.Task> UpdateStatus(string status, int taskId, CancellationToken cancellationToken);
}
