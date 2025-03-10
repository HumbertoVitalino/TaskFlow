using AutoBogus;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.TagUseCases.Output;
using Core.UseCases.TaskUseCases.AddTagToTask;
using Core.UseCases.TaskUseCases.AddTagToTask.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using Moq;

namespace UnitTests.TaskUseCasesTest;

public class AddTagToTaskTestFixture
{
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<ITaskRepository> _taskRepository = new();
    private readonly Mock<ITagRepository> _tagRepository = new();
    private readonly AddTagToTask _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public AddTagToTaskTestFixture()
    {
        _useCase = SetupAddTagToTaskUseCase();
    }

    private AddTagToTask SetupAddTagToTaskUseCase()
    {
        _mapper
            .Setup(m => m.Map<TaskOutput>(It.IsAny<Core.Entities.Task>()))
            .Returns((Core.Entities.Task task) => new TaskOutput
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Tag = task.Tag != null ? new TagOutput { Id = task.Tag.Id, Name = task.Tag.Name } : null
            });

        _unitOfWork
            .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
            .Returns(System.Threading.Tasks.Task.CompletedTask);

        return new AddTagToTask(_mapper.Object, _unitOfWork.Object, _taskRepository.Object, _tagRepository.Object);
    }

    public async Task<TaskOutput> HandleUseCaseAsync(AddTagToTaskInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public void SetupTaskFound(int taskId)
    {
        var task = new AutoFaker<Core.Entities.Task>()
            .RuleFor(t => t.Id, _ => taskId)
            .RuleFor(t => t.Tag, _ => null)
            .Generate();

        _taskRepository
            .Setup(repo => repo.Get(taskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);
    }

    public void SetupTaskNotFound(int taskId)
    {
        _taskRepository
            .Setup(repo => repo.Get(taskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Core.Entities.Task)null!);
    }

    public void SetupTagFound(int tagId)
    {
        var tag = new AutoFaker<Tag>()
            .RuleFor(t => t.Id, _ => tagId)
            .Generate();

        _tagRepository
            .Setup(repo => repo.Get(tagId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tag);
    }

    public AddTagToTaskInput SetupValidInput()
        => new AutoFaker<AddTagToTaskInput>()
            .RuleFor(x => x.Id, f => f.Random.Int(1, 1000))
            .RuleFor(x => x.IdTag, f => f.Random.Int(1, 1000))
            .Generate();
}

