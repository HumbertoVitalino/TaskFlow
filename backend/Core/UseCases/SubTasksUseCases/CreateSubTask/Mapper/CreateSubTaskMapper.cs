using AutoMapper;
using Core.Entities;
using Core.UseCases.SubTasksUseCases.CreateSubTask.Boundaries;
using Core.UseCases.SubTasksUseCases.Output;

namespace Core.UseCases.SubTasksUseCases.CreateSubTask.Mapper;

public sealed class CreateSubTaskMapper : Profile
{
    public CreateSubTaskMapper()
    {
        CreateMap<CreateSubTaskInput, SubTask>();
        CreateMap<SubTask, SubTaskOutput>()
            .ForMember(x => x.TaskId, a => a.MapFrom(t => t.Task.Id));
    }
}
