using AutoMapper;
using Core.Entities;
using Core.UseCases.TagUseCases.CreateTag.Boundaries;
using Core.UseCases.TagUseCases.Output;

namespace Core.UseCases.TagUseCases.CreateTag.Mapper;

public sealed class CreateTagMapper : Profile
{
    public CreateTagMapper()
    {
        CreateMap<CreateTagInput, Tag>();
        CreateMap<Tag, TagOutput>();        
    }
}
