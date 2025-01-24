using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.TagUseCases.CreateTag.Boundaries;
using Core.UseCases.TagUseCases.Output;
using MediatR;

namespace Core.UseCases.TagUseCases.CreateTag;

public class CreateTag : IRequestHandler<CreateTagInput, TagOutput>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITagRepository _tagRepository;

    public CreateTag(IUnitOfWork unitOfWork, IMapper mapper, ITagRepository tagRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _tagRepository = tagRepository;
    }

    public async Task<TagOutput> Handle(CreateTagInput input, CancellationToken cancellationToken)
    {
        var tag = _mapper.Map<Tag>(input);

        _tagRepository.Create(tag);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<TagOutput>(tag);
    }
}
