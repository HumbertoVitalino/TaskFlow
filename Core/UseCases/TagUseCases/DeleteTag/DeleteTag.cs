using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TagUseCases.DeleteTag.Boundaries;
using Core.UseCases.TagUseCases.Output;
using MediatR;

namespace Core.UseCases.TagUseCases.DeleteTag;

public sealed class DeleteTag : IRequestHandler<DeleteTagInput, TagOutput>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITagRepository _tagRepository;

    public DeleteTag(IUnitOfWork unitOfWork, IMapper mapper, ITagRepository tagRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tagRepository = tagRepository;
    }

    public async Task<TagOutput> Handle(DeleteTagInput input, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.Get(input.Id, cancellationToken);

        if (tag.UserId != input.UserId)
            return default!;

        if (tag == null) return default!;

        _tagRepository.Delete(tag);
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<TagOutput>(tag);
    }
}
