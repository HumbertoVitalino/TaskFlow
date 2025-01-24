using Core.UseCases.TagUseCases.Output;
using MediatR;

namespace Core.UseCases.TagUseCases.CreateTag.Boundaries;

public sealed record CreateTagInput(string Name, string Description): IRequest<TagOutput>;
