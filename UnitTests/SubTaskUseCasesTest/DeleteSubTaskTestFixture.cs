using AutoBogus;
using AutoMapper;
using Core.Interfaces;
using Core.UseCases.SubTasksUseCases.DeleteSubTask;
using Core.UseCases.SubTasksUseCases.DeleteSubTask.Boundaries;
using Core.UseCases.SubTasksUseCases.Output;
using Moq;

namespace UnitTests.SubTaskUseCasesTest;

public class DeleteSubTaskTestFixture
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<ISubTaskRepository> _subTaskRepository = new();
    private readonly DeleteSubTask _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public DeleteSubTaskTestFixture()
    {
        _useCase = SetupDeleteSubTaskUseCase();
    }

    private DeleteSubTask SetupDeleteSubTaskUseCase()
    {
        _mapper
            .Setup(m => m.Map<SubTaskOutput>(It.IsAny<Core.Entities.SubTask>()))
            .Returns((Core.Entities.SubTask subtask) => new SubTaskOutput
            {
                Id = subtask.Id,
                Title = subtask.Title,
                Status = subtask.Status
            });

        _unitOfWork
            .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask);

        return new DeleteSubTask(_subTaskRepository.Object, _unitOfWork.Object, _mapper.Object);
    }

    public async Task<SubTaskOutput> HandleUseCaseAsync(DeleteSubTaskInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public void SetupSubTaskFound(int subTaskId)
    {
        var subTask = new AutoFaker<Core.Entities.SubTask>()
            .RuleFor(s => s.Id, _ => subTaskId)
            .Generate();

        _subTaskRepository
            .Setup(repo => repo.Get(subTaskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(subTask);
    }

    public void SetupSubTaskNotFound(int subTaskId)
    {
        _subTaskRepository
            .Setup(repo => repo.Get(subTaskId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Core.Entities.SubTask)null!);
    }

    public DeleteSubTaskInput SetupValidInput()
        => new AutoFaker<DeleteSubTaskInput>()
            .RuleFor(x => x.Id, f => f.Random.Int(1, 1000))
            .Generate();
}
