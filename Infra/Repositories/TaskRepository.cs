using Core.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories
{
    public class TaskRepository : BaseRepository<Core.Entities.Task>, ITaskRepository
    {
        public TaskRepository(AppDbContext context) : base(context) { }

        public async Task<List<Core.Entities.Task>> GetAllWithTag(CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .Include(t => t.Tag)
                .Include(s => s.SubTasks)
                .Include(u => u.User)
                .ToListAsync();
        }

        public async Task<List<Core.Entities.Task>> GetTasksByTag(int tagId, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .Where(x => x.Tag.Id == tagId)
                .ToListAsync();
        }

        public async Task<Core.Entities.Task> GetWithFK(int id, CancellationToken cancellationToken)
        {
            return await _context.Tasks
                .Include(t => t.Tag)
                .Include(s => s.SubTasks)
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }
    }
}
