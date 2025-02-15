using Core.Entities;
using Core.Interfaces;
using Infra.Context;

namespace Infra.Repositories;

public class SubTaskRepository : BaseRepository<SubTask>, ISubTaskRepository
{
    public SubTaskRepository(AppDbContext context) : base(context) { }
}
