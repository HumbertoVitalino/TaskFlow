using FluentAssertions;

namespace UnitTests.TaskUseCasesTest;

public class UpdateTaskTest : IClassFixture<UpdateTaskTestFixture>
{
    private readonly UpdateTaskTestFixture _fixture;

    public UpdateTaskTest(UpdateTaskTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Handle should update task successfully")]
    public async Task Handle_Should_Update_Task_Successfully()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTaskFound(input.Id);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(input.Id);
        result.Title.Should().Be(input.Title);
        result.Description.Should().Be(input.Description);
        result.DueDate.Should().Be(input.DueDate);
    }

    [Fact(DisplayName = "Handle should return null when task is not found")]
    public async Task Handle_Should_Return_Null_When_Task_Is_Not_Found()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTaskNotFound(input.Id);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().BeNull();
    }
}
