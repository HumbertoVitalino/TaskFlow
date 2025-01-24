using Core.Entities;
using Core.Interfaces;
using Infra.Context;

namespace Infra.Repositories;

public class TagRepository : BaseRepository<Tag>, ITagRepository
{
    public TagRepository(AppDbContext context) : base(context) { }

}
