using Core.Enums;
using FluentAssertions;

namespace UnitTests.SubTasksUseCasesTest;

public class UpdateSubTaskTest
{
    private readonly UpdateSubTaskTestFixture _fixture = new();

    [Fact(DisplayName = "Handle Should Update SubTask When SubTask Exists")]
    public async Task Handle_ShouldUpdateSubTask_WhenSubTaskExists()
    {
        // Arrange
        var input = _fixture.SetupValidInput(StatusEnum.Pending);
        _fixture.SetupSubTaskFound(input.IdSubTask);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(input.IdSubTask);
        result.Title.Should().Be(input.Title);
        result.Status.Should().Be(input.Status);
        result.DueDate.Should().Be(input.DueDate);
    }

    [Fact(DisplayName = "Handle Should Throw Exception When SubTask Not Found")]
    public async Task Handle_ShouldThrowException_WhenSubTaskNotFound()
    {
        // Arrange
        var input = _fixture.SetupValidInput(StatusEnum.Pending);
        _fixture.SetupSubTaskNotFound(input.IdSubTask);

        // Act
        var act = async () => await _fixture.HandleUseCaseAsync(input);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"SubTask with key {input.IdSubTask} not found.");
    }

    [Fact(DisplayName = "Handle Should Update Task Status When All SubTasks completed")]
    public async Task Handle_ShouldUpdateTaskStatus_WhenAllSubTasksCompleted()
    {
        // Arrange
        var input = _fixture.SetupValidInput(StatusEnum.Completed);

        _fixture.SetupSubTaskFound(input.IdSubTask);

        var task = new Core.Entities.Task
        {
            Id = 1,
            SubTasks = new List<Core.Entities.SubTask>
            {
                new() { Id = 2, Status = StatusEnum.Completed },
                new() { Id = 3, Status = StatusEnum.Completed }
            }
        };

        _fixture.SetupTaskWithSubTasks(task);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        task.Status.Should().Be(StatusEnum.Completed);
    }

    [Fact(DisplayName = "Handle Should Update Task Status When At Least One SubTask In Progress")]
    public async Task Handle_ShouldUpdateTaskStatus_WhenAtLeastOneSubTaskInProgress()
    {
        // Arrange
        var input = _fixture.SetupValidInput(StatusEnum.InProgress);

        _fixture.SetupSubTaskFound(input.IdSubTask);

        var task = new Core.Entities.Task
        {
            Id = 1,
            SubTasks = new List<Core.Entities.SubTask>
            {
                new() { Id = 2, Status = StatusEnum.InProgress },
                new() { Id = 3, Status = StatusEnum.Completed }
            }
        };

        _fixture.SetupTaskWithSubTasks(task);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        task.Status.Should().Be(StatusEnum.InProgress);
    }
}
