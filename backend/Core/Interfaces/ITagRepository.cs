using Core.Entities;

namespace Core.Interfaces;

public interface ITagRepository : IBaseRepository<Tag>
{
    Task <List<Tag>> GetAllByUserId(int userId, CancellationToken cancellationToken);
}
