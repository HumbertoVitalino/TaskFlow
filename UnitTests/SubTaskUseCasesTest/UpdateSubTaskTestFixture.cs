using AutoBogus;
using AutoMapper;
using Core.Enums;
using Core.Interfaces;
using Core.UseCases.SubTasksUseCases.Output;
using Core.UseCases.SubTasksUseCases.UpdateSubTask;
using Core.UseCases.SubTasksUseCases.UpdateSubTask.Boundaries;
using Moq;

namespace UnitTests.SubTasksUseCasesTest;

public class UpdateSubTaskTestFixture
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<ISubTaskRepository> _subTaskRepository = new();
    private readonly Mock<ITaskRepository> _taskRepository = new();
    private readonly UpdateSubTask _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public UpdateSubTaskTestFixture()
    {
        _useCase = SetupUpdateSubTaskUseCase();
    }

    private UpdateSubTask SetupUpdateSubTaskUseCase()
    {
        _mapper
            .Setup(m => m.Map<SubTaskOutput>(It.IsAny<Core.Entities.SubTask>()))
            .Returns((Core.Entities.SubTask subtask) => new SubTaskOutput
            {
                Id = subtask.Id,
                Title = subtask.Title,
                Status = subtask.Status,
                DueDate = subtask.DueDate
            });

        _unitOfWork
            .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        return new UpdateSubTask(_subTaskRepository.Object, _taskRepository.Object, _unitOfWork.Object, _mapper.Object);
    }

    public async Task<SubTaskOutput> HandleUseCaseAsync(UpdateSubTaskInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public void SetupSubTaskFound(int subTaskId)
    {
        var task = new Core.Entities.Task
        {
            Id = 1,
            SubTasks = new List<Core.Entities.SubTask>()
        };

        var subTask = new Core.Entities.SubTask
        {
            Id = subTaskId,
            Status = StatusEnum.Pending,
            Task = task
        };

        task.SubTasks.Add(subTask);

        _subTaskRepository
            .Setup(repo => repo.GetWithTask(subTaskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(subTask);
    }


    public void SetupSubTaskNotFound(int subTaskId)
    {
        _subTaskRepository
            .Setup(repo => repo.GetWithTask(subTaskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Core.Entities.SubTask)null!);
    }

    public void SetupTaskWithSubTasks(Core.Entities.Task task)
    {
        foreach (var subTask in task.SubTasks)
        {
            subTask.Task = task;
        }

        _taskRepository
            .Setup(repo => repo.GetWithFK(task.Id, It.IsAny<CancellationToken>()))
            .ReturnsAsync(task);
    }


    public UpdateSubTaskInput SetupValidInput(StatusEnum status)
    => new AutoFaker<UpdateSubTaskInput>()
        .RuleFor(x => x.IdSubTask, f => f.Random.Int(1, 1000))
        .RuleFor(x => x.Title, f => f.Lorem.Sentence(3))
        .RuleFor(x => x.DueDate, f => f.Date.Future())
        .RuleFor(x => x.Status, _ => status)
        .Generate();
}
