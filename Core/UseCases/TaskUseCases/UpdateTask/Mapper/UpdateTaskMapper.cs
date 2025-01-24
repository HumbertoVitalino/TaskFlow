using AutoMapper;
using Core.UseCases.TaskUseCases.Output;
using Core.UseCases.TaskUseCases.UpdateTask.Boundaries;

namespace Core.UseCases.TaskUseCases.UpdateTask.Mapper;

public sealed class UpdateTaskMapper : Profile
{
    public UpdateTaskMapper()
    {
        CreateMap<UpdateTaskInput, Entities.Task>();
        CreateMap<Entities.Task, TaskOutput>();
    }
}
