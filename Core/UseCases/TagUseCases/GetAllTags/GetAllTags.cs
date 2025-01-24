using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TagUseCases.GetAllTags.Boundaries;
using Core.UseCases.TagUseCases.Output;
using MediatR;

namespace Core.UseCases.TagUseCases.GetAllTags;

public sealed class GetAllTags : IRequestHandler<GetAllTagsInput, List<TagOutput>>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public GetAllTags(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }

    public async Task<List<TagOutput>> Handle(GetAllTagsInput input, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetAll(cancellationToken);
        return _mapper.Map<List<TagOutput>>(tags);
    }
}
