using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.AddTagToTask.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.AddTagToTask;

public class AddTagToTask : IRequestHandler<AddTagToTaskInput, TaskTagOutput>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ITaskRepository _taskRepository;
    private readonly ITagRepository _tagRepository;

    public AddTagToTask(IUnitOfWork unitOfWork, IMapper mapper, ITaskRepository taskRepository, ITagRepository tagRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _taskRepository = taskRepository;
        _tagRepository = tagRepository;
    }

    public async Task<TaskTagOutput> Handle(AddTagToTaskInput input, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.Get(input.TaskId, cancellationToken);

        if (task is null)
            return new TaskTagOutput(false, "Task not found");

        var tag = await _tagRepository.Get(input.TagId, cancellationToken);

        if (tag is null)
            return new TaskTagOutput(false, "One or more tags not found");        

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<TaskTagOutput>(task);
    }
}
