using AutoBogus;
using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.Output;
using Core.UseCases.TaskUseCases.UpdateTask;
using Core.UseCases.TaskUseCases.UpdateTask.Boundaries;
using Moq;

namespace UnitTests.TaskUseCasesTest;

public class UpdateTaskTestFixture
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<ITaskRepository> _taskRepository = new();
    private readonly UpdateTask _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public UpdateTaskTestFixture()
    {
        _useCase = SetupUpdateTaskUseCase();
    }

    private UpdateTask SetupUpdateTaskUseCase()
    {
        _mapper
            .Setup(m => m.Map<TaskOutput>(It.IsAny<Core.Entities.Task>()))
            .Returns((Core.Entities.Task task) => new TaskOutput
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                DueDate = task.DueDate
            });

        _unitOfWork
            .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
            .Returns(System.Threading.Tasks.Task.CompletedTask);

        return new UpdateTask(_unitOfWork.Object, _mapper.Object, _taskRepository.Object);
    }

    public async Task<TaskOutput> HandleUseCaseAsync(UpdateTaskInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public void SetupTaskFound(int taskId)
    {
        var task = new AutoFaker<Core.Entities.Task>()
            .RuleFor(t => t.Id, _ => taskId)
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

    public UpdateTaskInput SetupValidInput()
        => new AutoFaker<UpdateTaskInput>()
            .RuleFor(x => x.Id, f => f.Random.Int(1, 1000))
            .RuleFor(x => x.Title, f => f.Lorem.Sentence(3))
            .RuleFor(x => x.Description, f => f.Lorem.Paragraph())
            .RuleFor(x => x.DueDate, f => f.Date.Future())
            .Generate();
}
