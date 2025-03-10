using AutoBogus;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.GetAllTasks;
using Core.UseCases.TaskUseCases.GetAllTasks.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using Moq;

namespace UnitTests.TaskUseCasesTest;

public class GetAllTasksTestFixture
{
    private readonly Mock<ITaskRepository> _taskRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly GetAllTasks _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public GetAllTasksTestFixture()
    {
        _useCase = SetupGetAllTasksUseCase();
    }

    private GetAllTasks SetupGetAllTasksUseCase()
    {
        _mapper
            .Setup(m => m.Map<List<TaskOutput>>(It.IsAny<List<Core.Entities.Task>>()))
            .Returns((List<Core.Entities.Task> tasks) => tasks.Select(t => new TaskOutput
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description
            }).ToList());

        return new GetAllTasks(_taskRepository.Object, _mapper.Object);
    }

    public async Task<List<TaskOutput>> HandleUseCaseAsync(GetAllTasksInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public void SetupTasksFound(int userId)
    {
        var tasks = new AutoFaker<Core.Entities.Task>()
            .RuleFor(t => t.Id, f => f.Random.Int(1, 1000))
            .RuleFor(i => i.UserId, _ => userId)
            .Generate(5);

        _taskRepository
            .Setup(repo => repo.GetAllWithTag(userId ,It.IsAny<CancellationToken>()))
            .ReturnsAsync(tasks);
    }

    public void SetupTasksNotFound(int userId)
    {
        _taskRepository
            .Setup(repo => repo.GetAllWithTag(userId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Core.Entities.Task>());
    }

    public GetAllTasksInput SetupValidInput()
        => new AutoFaker<GetAllTasksInput>().Generate();
}
