namespace Core.Entities;

public abstract class BaseEntity
{
    public int Id { get; set; }
    public DateTime DateCreated { get; set; } 
    public DateTime DateUpdated { get; set; }
    public DateTime DateDeleted { get; set; }
}
