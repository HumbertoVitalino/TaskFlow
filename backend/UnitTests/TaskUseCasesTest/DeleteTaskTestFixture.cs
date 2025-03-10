using AutoBogus;
using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.DeleteTask;
using Core.UseCases.TaskUseCases.DeleteTask.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using Moq;

namespace UnitTests.TaskUseCasesTest;

public class DeleteTaskTestFixture
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<ITaskRepository> _taskRepository = new();
    private readonly Mock<IMapper> _mapperMock = new();
    private readonly DeleteTask _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public DeleteTaskTestFixture()
    {
        _useCase = SetupDeleteTaskUseCase();
    }

    private DeleteTask SetupDeleteTaskUseCase()
    {
        _unitOfWork
            .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        _mapperMock
            .Setup(m => m.Map<TaskOutput>(It.IsAny<Core.Entities.Task>()))
            .Returns((Core.Entities.Task task) => new TaskOutput
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description
            });

        return new DeleteTask(
            _unitOfWork.Object,
            _taskRepository.Object,
            _mapperMock.Object
        );
    }

    public async Task<TaskOutput> HandleUseCaseAsync(DeleteTaskInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public DeleteTaskInput SetupValidInput()
        => new AutoFaker<DeleteTaskInput>()
            .RuleFor(x => x.Id, _ => new Random().Next(1, 1000))
            .Generate();

    public void SetupTaskExists()
    {
        _taskRepository
            .Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((int id, CancellationToken _) => new Core.Entities.Task
            {
                Id = id,
                Title = "Sample Task",
                Description = "This is a test task"
            });
    }

    public void SetupTaskNotFound()
    {
        _taskRepository
            .Setup(repo => repo.Get(It.IsAny<int>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Core.Entities.Task)null);
    }
}
