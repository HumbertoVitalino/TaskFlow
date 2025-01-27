namespace Core.Interfaces;

public interface ITaskRepository : IBaseRepository<Entities.Task>
{ 
    Task<List<Entities.Task>> GetAllWithTag(CancellationToken cancellationToken);
}
