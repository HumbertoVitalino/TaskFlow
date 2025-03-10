using AutoMapper;
using Core.UseCases.TaskUseCases.AddTagToTask.Boundaries;
using Core.UseCases.TaskUseCases.Output;

namespace Core.UseCases.TaskUseCases.AddTagToTask.Mapper;

public sealed class AddTagToTaskMapper : Profile
{
    public AddTagToTaskMapper()
    {
        CreateMap<AddTagToTaskInput, Task>();
        CreateMap<Task, TaskOutput>();
    }
}
