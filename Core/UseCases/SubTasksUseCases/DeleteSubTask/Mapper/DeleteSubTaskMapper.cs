using AutoMapper;
using Core.Entities;
using Core.UseCases.SubTasksUseCases.DeleteSubTask.Boundaries;
using Core.UseCases.SubTasksUseCases.Output;

namespace Core.UseCases.SubTasksUseCases.DeleteSubTask.Mapper;

public sealed class DeleteSubTaskMapper : Profile
{
    public DeleteSubTaskMapper()
    {
        CreateMap<DeleteSubTaskInput, SubTask>();
        CreateMap<SubTask, SubTaskOutput>();
    }
}
