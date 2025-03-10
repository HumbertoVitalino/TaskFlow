using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.SubTasksUseCases.CreateSubTask.Boundaries;
using Core.UseCases.SubTasksUseCases.Output;
using MediatR;

namespace Core.UseCases.SubTasksUseCases.CreateSubTask;

public class CreateSubTask : IRequestHandler<CreateSubTaskInput, SubTaskOutput>
{
    private readonly ITaskRepository _taskRepository;
    private readonly ISubTaskRepository _subTaskRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateSubTask(ITaskRepository taskRepository, ISubTaskRepository subTaskRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _taskRepository = taskRepository;
        _subTaskRepository = subTaskRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<SubTaskOutput> Handle(CreateSubTaskInput input, CancellationToken cancellationToken)
    {
        var subtask = _mapper.Map<SubTask>(input);

        var task = await _taskRepository.Get(input.TaskId, cancellationToken);

        if (task is null)
            throw new KeyNotFoundException($"Task with ID {input.TaskId} not found.");

        if (task.UserId != subtask.UserId)
            return default!;

        subtask.Task = task;

        _subTaskRepository.Create(subtask);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<SubTaskOutput>(subtask);
    }
}
