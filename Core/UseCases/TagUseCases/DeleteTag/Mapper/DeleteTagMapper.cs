using AutoMapper;
using Core.Entities;
using Core.UseCases.TagUseCases.DeleteTag.Boundaries;
using Core.UseCases.TagUseCases.Output;

namespace Core.UseCases.TagUseCases.DeleteTag.Mapper;

public sealed class DeleteTagMapper : Profile
{
    public DeleteTagMapper()
    {
        CreateMap<DeleteTagInput, Tag>();
        CreateMap<Tag, TagOutput>();
    }
}
