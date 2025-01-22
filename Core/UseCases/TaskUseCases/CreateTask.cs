using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.Boundaries;
using MediatR;

namespace Core.UseCases.TaskUseCases;

public class CreateTask : IRequestHandler<CreateTaskRequest, CreateTaskResponse>
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

    public async Task<CreateTaskResponse> Handle(CreateTaskRequest request, CancellationToken cancellationToken)
    {
        var task = _mapper.Map<Entities.Task>(request);

        _taskRepository.Create(task);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<CreateTaskResponse>(task);
    }
}
