using AutoMapper;
using Core.Entities;
using Core.UseCases.SubTasksUseCases.Output;
using Core.UseCases.SubTasksUseCases.UpdateSubTask.Boundaries;

namespace Core.UseCases.SubTasksUseCases.UpdateSubTask.Mapper;

public sealed class UpdateSubTaskMapper : Profile
{
    public UpdateSubTaskMapper()
    {
        CreateMap<UpdateSubTaskInput, SubTask>();
        CreateMap<SubTask, SubTaskOutput>();
    }
}
