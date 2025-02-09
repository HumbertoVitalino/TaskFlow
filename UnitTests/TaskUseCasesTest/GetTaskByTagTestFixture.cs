using AutoBogus;
using AutoMapper;
using Core.Interfaces;
using Core.UseCases.TaskUseCases.GetTasksByTag;
using Core.UseCases.TaskUseCases.GetTasksByTag.Boundaries;
using Core.UseCases.TaskUseCases.Output;
using Moq;

namespace UnitTests.TaskUseCasesTest;

public class GetTasksByTagTestFixture
{
    private readonly Mock<ITaskRepository> _taskRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly GetTasksByTag _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public GetTasksByTagTestFixture()
    {
        _useCase = SetupGetTasksByTagUseCase();
    }

    private GetTasksByTag SetupGetTasksByTagUseCase()
    {
        _mapper
            .Setup(m => m.Map<List<TaskOutput>>(It.IsAny<List<Core.Entities.Task>>()))
            .Returns((List<Core.Entities.Task> tasks) => tasks.Select(t => new TaskOutput
            {
                Id = t.Id,
                Title = t.Title,
                Description = t.Description
            }).ToList());

        return new GetTasksByTag(_taskRepository.Object, _mapper.Object, _unitOfWork.Object);
    }

    public async Task<List<TaskOutput>> HandleUseCaseAsync(GetTasksByTagInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public void SetupTasksFound(int tagId)
    {
        var tasks = new AutoFaker<Core.Entities.Task>()
            .RuleFor(t => t.Id, f => f.Random.Int(1, 1000))
            .RuleFor(t => t.Tag, _ => new Core.Entities.Tag { Id = tagId })
            .Generate(3);

        _taskRepository
            .Setup(repo => repo.GetTasksByTag(tagId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tasks);
    }

    public void SetupTasksNotFound(int tagId)
    {
        _taskRepository
            .Setup(repo => repo.GetTasksByTag(tagId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Core.Entities.Task>());
    }

    public GetTasksByTagInput SetupValidInput()
        => new AutoFaker<GetTasksByTagInput>()
            .RuleFor(x => x.idTag, f => f.Random.Int(1, 1000))
            .Generate();
}
