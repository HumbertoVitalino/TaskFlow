using Core.Entities;
using Core.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class SubTaskRepository : BaseRepository<SubTask>, ISubTaskRepository
{
    public SubTaskRepository(AppDbContext context) : base(context) { }

    public async Task<SubTask> GetWithTask(int id, CancellationToken cancellationToken)
    {
        return await _context.SubTasks.Include(s => s.Task).FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
