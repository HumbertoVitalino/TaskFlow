using Core.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class TaskRepository : BaseRepository<Core.Entities.Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }

        public async Task<Core.Entities.Task> UpdateStatus(string status, int taskId, CancellationToken cancellationToken)
        {
            var task = await _context.Set<Core.Entities.Task>().FirstOrDefaultAsync(t => t.Id == taskId, cancellationToken);

            if (task == null)
            {
                throw new KeyNotFoundException("Task not found.");
            }

            task.Status = status;

            _context.Set<Core.Entities.Task>().Update(task);

            return task;
        }
    }
}
