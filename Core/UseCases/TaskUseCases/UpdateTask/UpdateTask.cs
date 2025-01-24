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
        var task = await _taskRepository.Get(input.Id, cancellationToken);

        if (task == null) return default!;

        task.Title = input.Title;
        task.Description = input.Description;
        task.DueDate = input.DueDate;

        _taskRepository.Update(task);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<TaskOutput>(task);
    }
}
