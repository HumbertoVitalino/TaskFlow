using AutoMapper;
using Core.Entities;
using Core.UseCases.TagUseCases.GetAllTags.Boundaries;
using Core.UseCases.TagUseCases.Output;

namespace Core.UseCases.TagUseCases.GetAllTags.Mapper;

public sealed class GetAllTagsMapper : Profile
{
    public GetAllTagsMapper()
    {
        CreateMap<GetAllTagsInput, Tag>();
        CreateMap<Tag, TagOutput>();
    }
}
