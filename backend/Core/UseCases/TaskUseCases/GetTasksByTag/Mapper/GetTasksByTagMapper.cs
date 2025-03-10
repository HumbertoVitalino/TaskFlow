using AutoMapper;
using Core.UseCases.TaskUseCases.GetTasksByTag.Boundaries;
using Core.UseCases.TaskUseCases.Output;

namespace Core.UseCases.TaskUseCases.GetTasksByTag.Mapper;

public sealed class GetTasksByTagMapper : Profile
{
    public GetTasksByTagMapper()
    {
        CreateMap<GetTasksByTagInput, Entities.Task>();
        CreateMap<Entities.Task, TaskOutput>();
    }
}
