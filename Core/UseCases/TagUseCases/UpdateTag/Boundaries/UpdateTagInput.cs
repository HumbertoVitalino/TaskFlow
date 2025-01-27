using Core.UseCases.TagUseCases.Output;
using MediatR;

namespace Core.UseCases.TagUseCases.UpdateTag.Boundaries;

public sealed record UpdateTagInput(int Id, string Name, string? Description) : IRequest<TagOutput>;
