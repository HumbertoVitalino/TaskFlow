using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.AddTagToTask.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using MediatR;

namespace Core.UseCases.TaskUseCases.AddTagToTask;

public class AddTagToTask : IRequestHandler<AddTagToTaskInput, TaskOutput>
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly ITaskRepository _taskRepository;
    private readonly ITagRepository _tagRepository;

    public AddTagToTask(IMapper mapper, IUnitOfWork unitOfWork, ITaskRepository taskRepository, ITagRepository tagRepository)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _taskRepository = taskRepository;
        _tagRepository = tagRepository;
    }

    public async Task<TaskOutput> Handle(AddTagToTaskInput input, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.Get(input.Id, cancellationToken);
        if (task is null)
            return default!;

        var tag = await _tagRepository.Get(input.IdTag, cancellationToken);
        if (task.Tag is null)
            task.Tag = new Tag();

        task.Tag = tag;

        _taskRepository.Update(task);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<TaskOutput>(task);
    }
}
