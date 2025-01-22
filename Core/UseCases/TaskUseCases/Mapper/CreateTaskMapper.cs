using AutoMapper;
using Core.UseCases.TaskUseCases.Boundaries;

namespace Core.UseCases.TaskUseCases.Mapper;

public sealed class CreateTaskMapper : Profile
{
    public CreateTaskMapper()
    {
        CreateMap<CreateTaskRequest, Entities.Task>();
        CreateMap<Entities.Task, CreateTaskResponse>();
    }
}
