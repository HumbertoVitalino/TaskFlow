namespace Core.Requests;

public sealed record CreateSubTaskRequest(string Title, DateTime DueDate, int TaskId);
