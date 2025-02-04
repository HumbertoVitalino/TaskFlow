using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.GetTasksByTag.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.GetTasksByTag;

public class GetTasksByTag : IRequestHandler<GetTasksByTagInput, List<TaskOutput>>
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public GetTasksByTag(ITaskRepository taskRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<List<TaskOutput>> Handle(GetTasksByTagInput input, CancellationToken cancellationToken)
    {
        var tasks = await _taskRepository.GetTasksByTag(input.idTag, cancellationToken);

        if (tasks is null)
            return default!;

        return _mapper.Map<List<TaskOutput>>(tasks);
    }
}
