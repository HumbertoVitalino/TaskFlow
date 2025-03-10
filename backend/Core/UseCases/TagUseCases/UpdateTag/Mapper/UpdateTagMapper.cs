using AutoMapper;
using Core.Entities;
using Core.UseCases.TagUseCases.Output;
using Core.UseCases.TagUseCases.UpdateTag.Boundaries;

namespace Core.UseCases.TagUseCases.UpdateTag.Mapper;

public sealed class UpdateTagMapper : Profile
{
    public UpdateTagMapper()
    {
        CreateMap<UpdateTagInput, Tag>();
        CreateMap<Tag, TagOutput>();        
    }
}
