using AutoBogus;
using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.Boundaries;
using Core.UseCases.TaskUseCases.CreateTask;
using Core.UseCases.TaskUseCases.Output;
using Moq;

namespace UnitTests.TaskUseCasesTest;

public class CreateTaskTestFixture
{
    private readonly Mock<IUnitOfWork> _unitOfWorkMock = new();
    private readonly Mock<ITaskRepository> _taskRepositoryMock = new();
    private readonly Mock<ITagRepository> _tagRepositoryMock = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly CreateTask _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public CreateTaskTestFixture()
    {
        _useCase = SetupCreateTaskUseCase();
    }

    private CreateTask SetupCreateTaskUseCase()
    {
        _unitOfWorkMock
            .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        SetupMapperMock();

        _tagRepositoryMock
            .Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((int tagId, CancellationToken _) => new Core.Entities.Tag
            {
                Id = tagId,
                Name = $"Tag {tagId}"
            });

        return new CreateTask(
            _unitOfWorkMock.Object,
            _taskRepositoryMock.Object,
            _mapperMock.Object,
            _tagRepositoryMock.Object
        );
    }

    private void SetupMapperMock()
    {
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
                Description = task.Description
            });
    }

    public async Task<TaskOutput> HandleUseCaseAsync(CreateTaskInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public CreateTaskInput SetupValidInput()
        => new AutoFaker<CreateTaskInput>()
            .RuleFor(x => x.TagId, _ => new Random().Next(1, 1000))
            .Generate();

    public CreateTaskInput SetupValidInputWithoutTag()
        => new AutoFaker<CreateTaskInput>()
            .RuleFor(x => x.TagId, _ => null)
            .Generate();
}
