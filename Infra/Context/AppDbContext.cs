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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Core.Entities.Task>()
            .HasOne(t => t.User)
            .WithMany(u => u.Tasks)
            .HasForeignKey(t => t.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Core.Entities.Task>()
            .HasOne(t => t.Tag)
            .WithMany()
            .HasForeignKey(t => t.TagId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<SubTask>()
            .HasOne(st => st.Task)
            .WithMany(t => t.SubTasks)
            .HasForeignKey(st => st.TaskId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Core.Entities.Task>()
            .Property(t => t.Status)
            .HasConversion<string>();
    }
}
