using Core.Entities;

namespace Core.Interfaces;

public interface ISubTaskRepository : IBaseRepository<SubTask>
{
    Task<SubTask> GetWithTask(int id, CancellationToken cancellationToken);
}
