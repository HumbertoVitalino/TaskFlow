namespace Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateCreated { get; } = DateTime.Now; 
    public DateTime DateUpdated { get; set; }
    public DateTime DateDeleted { get; set; }
}
