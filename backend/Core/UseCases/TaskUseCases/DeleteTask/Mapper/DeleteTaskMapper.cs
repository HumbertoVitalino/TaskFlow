using AutoMapper;
using Core.UseCases.TaskUseCases.DeleteTask.Boundaries;
using Core.UseCases.TaskUseCases.Output;

namespace Core.UseCases.TaskUseCases.DeleteTask.Mapper;

public sealed class DeleteTaskMapper : Profile
{
    public DeleteTaskMapper()
    {
        CreateMap<DeleteTaskInput, Entities.Task>();
        CreateMap<Entities.Task, TaskOutput>();
    }
}
