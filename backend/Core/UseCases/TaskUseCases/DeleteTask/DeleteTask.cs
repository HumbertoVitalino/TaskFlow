using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.DeleteTask.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.DeleteTask;

public sealed class DeleteTask : IRequestHandler<DeleteTaskInput, TaskOutput>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public DeleteTask(IUnitOfWork unitOfWork, ITaskRepository taskRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<TaskOutput> Handle(DeleteTaskInput input, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.Get(input.Id, cancellationToken);

        if (task == null) return default!;

        if (task.UserId != input.UserId) return default!;

        _taskRepository.Delete(task);
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<TaskOutput>(task);
    }
}
