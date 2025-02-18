using AutoMapper;
using Core.Enums;
using Core.Interfaces;
using Core.UseCases.SubTasksUseCases.Output;
using Core.UseCases.SubTasksUseCases.UpdateSubTask.Boundaries;
using MediatR;

namespace Core.UseCases.SubTasksUseCases.UpdateSubTask;

public class UpdateSubTask : IRequestHandler<UpdateSubTaskInput, SubTaskOutput>
{
    private readonly ISubTaskRepository _subTaskRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateSubTask(ISubTaskRepository subTaskRepository, ITaskRepository taskRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _subTaskRepository = subTaskRepository;
        _taskRepository = taskRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SubTaskOutput> Handle(UpdateSubTaskInput input, CancellationToken cancellationToken)
    {
        var subtask = await _subTaskRepository.GetWithTask(input.IdSubTask, cancellationToken);

        if (subtask == null)
            throw new KeyNotFoundException($"SubTask with key {input.IdSubTask} not found.");
                
        if(input.Status.HasValue)
            subtask.Status = input.Status.Value;

        if (!string.IsNullOrWhiteSpace(input.Title))
            subtask.Title = input.Title;

        if (input.DueDate.HasValue)
            subtask.DueDate = input.DueDate.Value;

        _subTaskRepository.Update(subtask);

        var task = await _taskRepository.GetWithFK(subtask.Task.Id, cancellationToken);

        if (task is not null)
        {
            if (task.SubTasks.Any(x => x.Status == StatusEnum.InProgress))
            task.Status = StatusEnum.InProgress;

            if (task.SubTasks.All(x => x.Status == StatusEnum.Completed))
                task.Status = StatusEnum.Completed;
        }

        _taskRepository.Update(task);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<SubTaskOutput>(subtask);
    }
}
