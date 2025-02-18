using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infra.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<Core.Entities.Task> Tasks { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<SubTask> SubTasks { get; set; }
}
