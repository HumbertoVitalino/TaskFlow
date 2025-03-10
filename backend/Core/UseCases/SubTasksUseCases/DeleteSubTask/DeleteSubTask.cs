using AutoMapper;
using Core.Interfaces;
using Core.UseCases.SubTasksUseCases.DeleteSubTask.Boundaries;
using Core.UseCases.SubTasksUseCases.Output;
using MediatR;

namespace Core.UseCases.SubTasksUseCases.DeleteSubTask;

public class DeleteSubTask : IRequestHandler<DeleteSubTaskInput, SubTaskOutput>
{
    private readonly ISubTaskRepository _subTaskRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteSubTask(ISubTaskRepository subTaskRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _subTaskRepository = subTaskRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<SubTaskOutput> Handle(DeleteSubTaskInput input, CancellationToken cancellationToken)
    {
        var subtask = await _subTaskRepository.Get(input.Id, cancellationToken);

        if (subtask is null)
            return default!;

        if (subtask.UserId != input.UserId)
            return default!;

        _subTaskRepository.Delete(subtask);
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<SubTaskOutput>(subtask);
    }
}
