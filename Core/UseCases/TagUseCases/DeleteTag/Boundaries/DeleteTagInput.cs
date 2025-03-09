using Core.UseCases.TagUseCases.Output;
using MediatR;

namespace Core.UseCases.TagUseCases.DeleteTag.Boundaries;

public sealed record DeleteTagInput(int Id, int UserId) : IRequest<TagOutput>;