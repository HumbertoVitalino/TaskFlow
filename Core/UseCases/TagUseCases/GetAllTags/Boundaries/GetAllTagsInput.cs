using Core.UseCases.TagUseCases.Output;
using MediatR;

namespace Core.UseCases.TagUseCases.GetAllTags.Boundaries;

public sealed record GetAllTagsInput : IRequest<List<TagOutput>>;
