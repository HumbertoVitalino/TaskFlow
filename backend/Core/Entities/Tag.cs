﻿namespace Core.Entities;

public sealed class Tag : BaseEntity
{
    public string Name { get; set; }
    public string? Description { get; set; }
    public ICollection<Task> Tasks  { get; set; }
    public int UserId { get; set; }
    public User User { get; set; }
}
