using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TagUseCases.Output;
using Core.UseCases.TagUseCases.UpdateTag.Boundaries;
using MediatR;

namespace Core.UseCases.TagUseCases.UpdateTag;

public class UpdateTag : IRequestHandler<UpdateTagInput, TagOutput>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateTag(ITagRepository tagRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<TagOutput> Handle(UpdateTagInput input, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.Get(input.Id, cancellationToken);

        if (tag == null) return default!;

        if (tag.UserId != input.UserId) return default!;

        if (input.Name != null)
            tag.Name = input.Name;

        if (input.Description != null)
            tag.Description = input.Description;

        _tagRepository.Update(tag);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<TagOutput>(tag);
    }
}
