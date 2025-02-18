using AutoMapper;
using Core.Entities;
using Core.UseCases.TaskUseCases.Output;
using Core.UseCases.SubTasksUseCases.Output;

public sealed class GetAllTasksMapper : Profile
{
    public GetAllTasksMapper()
    {
        CreateMap<Core.Entities.Task, TaskOutput>();
        CreateMap<SubTask, SubTaskOutput>()
            .ForMember(dest => dest.TaskId, opt => opt.MapFrom(src => src.Task.Id));
    }
}

