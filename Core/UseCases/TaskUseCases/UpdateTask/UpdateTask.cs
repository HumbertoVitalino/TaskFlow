using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.Output;
using Core.UseCases.TaskUseCases.UpdateTask.Boundaries;
using MediatR;

namespace Core.UseCases.TaskUseCases.UpdateTask;

public class UpdateTask : IRequestHandler<UpdateTaskInput, TaskOutput>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;

    public UpdateTask(IUnitOfWork unitOfWork, IMapper mapper, ITaskRepository taskRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _taskRepository = taskRepository;
    }

    public async Task<TaskOutput> Handle(UpdateTaskInput input, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetWithFK(input.Id, cancellationToken);

        if (task == null) return default!;

        if (task.UserId != input.UserId)
            return default!;

        if (input.Title != null)
        {
            task.Title = input.Title;
        }

        if (input.Description != null)
        {
            task.Description = input.Description;
        }

        if (input.DueDate != null)
        {
            task.DueDate = input.DueDate.Value;
        }

        _taskRepository.Update(task);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<TaskOutput>(task);
    }
}
