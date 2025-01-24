using AutoMapper;
using Core.UseCases.TaskUseCases.GetAllTasks.Boundaries;
using Core.UseCases.TaskUseCases.Output;

namespace Core.UseCases.TaskUseCases.GetAllTasks.Mapper;

public sealed class GetAllTasksMapper : Profile
{
    public GetAllTasksMapper()
    {
        CreateMap<GetAllTasksInput, Entities.Task>();
        CreateMap<Entities.Task, TaskOutput>();
    }
}
