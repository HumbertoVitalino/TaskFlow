namespace Core.Interfaces;

public interface ITaskRepository : IBaseRepository<Entities.Task>
{
    Task<Entities.Task> UpdateStatus(int status, int taskId, CancellationToken cancellationToken);
}
