using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.GetAllTasks.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.GetAllTasks;

public sealed class GetAllTasks : IRequestHandler<GetAllTasksInput, List<TaskOutput>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;    

    public GetAllTasks(ITaskRepository taskRepository, IMapper mapper)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<List<TaskOutput>> Handle(GetAllTasksInput input, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetAll(cancellationToken);
        return _mapper.Map<List<TaskOutput>>(tasks);
    }
}
