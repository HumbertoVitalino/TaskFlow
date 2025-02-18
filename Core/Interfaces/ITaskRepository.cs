namespace Core.Interfaces;

public interface ITaskRepository : IBaseRepository<Entities.Task>
{ 
    Task<List<Entities.Task>> GetAllWithTag(CancellationToken cancellationToken);
    Task<List<Entities.Task>> GetTasksByTag(int tagId, CancellationToken cancellationToken);
    Task<Entities.Task> GetWithFK(int id,  CancellationToken cancellationToken);
}
