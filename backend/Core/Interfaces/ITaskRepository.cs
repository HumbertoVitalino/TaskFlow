﻿namespace Core.Interfaces;

public interface ITaskRepository : IBaseRepository<Entities.Task>
{ 
    Task<List<Entities.Task>> GetAllWithTag(int userId, CancellationToken cancellationToken);
    Task<List<Entities.Task>> GetTasksByTag(int tagId, int userId, CancellationToken cancellationToken);
    Task<Entities.Task> GetWithFK(int id,  CancellationToken cancellationToken);
}
