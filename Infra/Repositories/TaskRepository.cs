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
            return await _context.Tasks.Include(t => t.Tag).ToListAsync();
        }
    }
}
