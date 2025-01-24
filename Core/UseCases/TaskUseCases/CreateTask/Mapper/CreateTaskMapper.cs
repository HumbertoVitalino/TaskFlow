using AutoMapper;
using Core.UseCases.TaskUseCases.Boundaries;
using Core.UseCases.TaskUseCases.Output;

namespace Core.UseCases.TaskUseCases.Mapper;

public sealed class CreateTaskMapper : Profile
{
    public CreateTaskMapper()
    {
        CreateMap<CreateTaskInput, Entities.Task>();
        CreateMap<Entities.Task, TaskOutput>();
    }
}
