using FluentAssertions;

namespace UnitTests.SubTasksUseCasesTest;

public class CreateSubTaskTest : IClassFixture<CreateSubTaskTestFixture>
{
    private readonly CreateSubTaskTestFixture _fixture;

    public CreateSubTaskTest(CreateSubTaskTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Handle_ValidInput_ShouldCreateSubTask()
    {
        // Arrange
        int taskId = 1;
        _fixture.SetupTaskExists(taskId);
        var input = _fixture.SetupValidInput(taskId);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(input.Title);
        result.TaskId.Should().Be(taskId);

        _fixture.VerifyCreateCalled();
        _fixture.VerifyCommitCalled();
    }

    [Fact]
    public async Task Handle_TaskNotExists_ShouldThrowKeyNotFoundException()
    {
        // Arrange
        int taskId = 999;
        _fixture.SetupTaskNotExists(taskId);
        var input = _fixture.SetupValidInput(taskId);

        // Act
        Func<Task> act = async () => await _fixture.HandleUseCaseAsync(input);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>()
            .WithMessage($"Task with ID {taskId} not found.");
    }
}
