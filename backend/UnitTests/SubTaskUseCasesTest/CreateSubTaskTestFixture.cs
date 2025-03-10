using AutoBogus;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.SubTasksUseCases.CreateSubTask;
using Core.UseCases.SubTasksUseCases.CreateSubTask.Boundaries;
using Core.UseCases.SubTasksUseCases.Output;
using Moq;

namespace UnitTests.SubTasksUseCasesTest;

public class CreateSubTaskTestFixture
{
    private readonly Mock<ITaskRepository> _taskRepository = new();
    private readonly Mock<ISubTaskRepository> _subTaskRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly CreateSubTask _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public CreateSubTaskTestFixture()
    {
        _useCase = SetupCreateSubTaskUseCase();
    }

    private CreateSubTask SetupCreateSubTaskUseCase()
    {
        _unitOfWork
            .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
            .Returns(System.Threading.Tasks.Task.CompletedTask);

        _mapper
            .Setup(m => m.Map<SubTask>(It.IsAny<CreateSubTaskInput>()))
            .Returns((CreateSubTaskInput input) => new SubTask
            {
                Id = new Random().Next(1, 1000),
                Title = input.Title,
                DueDate = input.DueDate,
                Status = Core.Enums.StatusEnum.Pending
            });

        _mapper
            .Setup(m => m.Map<SubTaskOutput>(It.IsAny<SubTask>()))
            .Returns((SubTask subTask) => new SubTaskOutput
            {
                Id = subTask.Id,
                Title = subTask.Title,
                DueDate = subTask.DueDate,
                Status = subTask.Status,
                TaskId = subTask.Task.Id
            });

        return new CreateSubTask(
            _taskRepository.Object,
            _subTaskRepository.Object,
            _mapper.Object,
            _unitOfWork.Object);
    }

    public async Task<SubTaskOutput> HandleUseCaseAsync(CreateSubTaskInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public CreateSubTaskInput SetupValidInput(int taskId)
        => new AutoFaker<CreateSubTaskInput>()
        .RuleFor(x => x.TaskId, taskId)
        .Generate();

    public void SetupTaskExists(int taskId)
    {
        var task = new Core.Entities.Task { Id = taskId, Title = "Task Test", Status = Core.Enums.StatusEnum.Pending };
        _taskRepository.Setup(repo => repo.Get(taskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);
    }

    public void SetupTaskNotExists(int taskId)
    {
        _taskRepository.Setup(repo => repo.Get(taskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Core.Entities.Task)null!);
    }

    public void VerifyCreateCalled()
    {
        _subTaskRepository.Verify(repo => repo.Create(It.IsAny<SubTask>()), Times.Once);
    }

    public void VerifyCommitCalled()
    {
        _unitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }
}
