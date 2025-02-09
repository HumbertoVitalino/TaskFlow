using AutoBogus;
using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TagUseCases.Output;
using Core.UseCases.TaskUseCases.Boundaries;
using Core.UseCases.TaskUseCases.CreateTask;
using Core.UseCases.TaskUseCases.Output;
using Moq;

namespace UnitTests.TaskUseCasesTest;

public class CreateTaskTestFixture
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<ITaskRepository> _taskRepository = new();
    private readonly Mock<ITagRepository> _tagRepository = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly CreateTask _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public CreateTaskTestFixture()
    {
        _useCase = SetupCreateTaskUseCase();
    }

    private CreateTask SetupCreateTaskUseCase()
    {
        _unitOfWork
            .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _mapperMock
            .Setup(m => m.Map<Core.Entities.Task>(It.IsAny<CreateTaskInput>()))
            .Returns((CreateTaskInput input) => new Core.Entities.Task
            {
                Id = new Random().Next(1, 1000),
                Title = input.Title,
                Description = input.Description
            });

        _mapperMock
            .Setup(m => m.Map<TaskOutput>(It.IsAny<Core.Entities.Task>()))
            .Returns((Core.Entities.Task task) => new TaskOutput
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                Status = 0,
                Tag = task.Tag is not null ? new TagOutput { Id = task.Tag.Id, Name = task.Tag.Name } : null
            });

        _tagRepository
            .Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((int tagId, CancellationToken _) => new Core.Entities.Tag
            {
                Id = tagId,
                Name = $"Tag {tagId}"
            });

        return new CreateTask(
            _unitOfWork.Object,
            _taskRepository.Object,
            _mapperMock.Object,
            _tagRepository.Object
        );
    }

    public async Task<TaskOutput> HandleUseCaseAsync(CreateTaskInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public CreateTaskInput SetupValidInput()
        => new AutoFaker<CreateTaskInput>()
            .RuleFor(x => x.TagId, _ => 1) // TagId fixo para garantir que sempre há uma Tag válida
            .Generate();

    public CreateTaskInput SetupValidInputWithoutTag()
        => new AutoFaker<CreateTaskInput>()
            .RuleFor(x => x.TagId, _ => null)
            .Generate();
}
