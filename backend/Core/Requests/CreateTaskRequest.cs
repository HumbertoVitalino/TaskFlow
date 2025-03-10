namespace Core.Requests;

public sealed record CreateTaskRequest(string Title, string? Description, DateTime DueDate, int? TagId);
