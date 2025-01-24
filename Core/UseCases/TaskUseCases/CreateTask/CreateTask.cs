using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.CreateTask;

public class CreateTask : IRequestHandler<CreateTaskInput, TaskOutput>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepository _taskRepository;
    private readonly IMapper _mapper;

    public CreateTask(IUnitOfWork unitOfWork, ITaskRepository taskRepository, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _taskRepository = taskRepository;
        _mapper = mapper;
    }

    public async Task<TaskOutput> Handle(CreateTaskInput input, CancellationToken cancellationToken)
    {
        var task = _mapper.Map<Entities.Task>(input);

        _taskRepository.Create(task);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<TaskOutput>(task);
    }
}
