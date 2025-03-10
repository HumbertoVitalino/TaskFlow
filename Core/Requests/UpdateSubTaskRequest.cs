using Core.Enums;

namespace Core.Requests;

public sealed record UpdateSubTaskRequest(string? Title, DateTime? DueDate, StatusEnum? Status);
