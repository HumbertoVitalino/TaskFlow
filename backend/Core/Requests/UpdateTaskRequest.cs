namespace Core.Requests;

public sealed record UpdateTaskRequest(string? Title, string? Description, DateTime? DueDate);
