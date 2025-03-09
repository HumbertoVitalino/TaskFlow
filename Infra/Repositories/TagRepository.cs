using Core.Entities;
using Core.Interfaces;
using Infra.Context;
using Microsoft.EntityFrameworkCore;

namespace Infra.Repositories;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context) { }

    public async Task<List<Tag>> GetAllByUserId(int userId, CancellationToken cancellationToken)
    {
        return await _context.Tags
            .Where(u => u.UserId == userId)
            .ToListAsync();
    }
}
