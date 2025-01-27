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
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;

    public CreateTask(IUnitOfWork unitOfWork, ITaskRepository taskRepository, IMapper mapper, ITagRepository tagRepository)
    {
        _unitOfWork = unitOfWork;
        _taskRepository = taskRepository;
        _mapper = mapper;
        _tagRepository = tagRepository;
    }

    public async Task<TaskOutput> Handle(CreateTaskInput input, CancellationToken cancellationToken)
    {
        var task = _mapper.Map<Entities.Task>(input);

        if (input.TagId.HasValue)
        {
            var tag = await _tagRepository.Get(input.TagId, cancellationToken);

            if (tag is null)
                throw new KeyNotFoundException($"Tag with ID {input.TagId.Value} not found.");

            task.Tag = tag;
        }

        _taskRepository.Create(task);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<TaskOutput>(task);
    }
}
