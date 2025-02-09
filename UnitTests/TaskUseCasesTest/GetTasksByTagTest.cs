using FluentAssertions;

namespace UnitTests.TaskUseCasesTest;

public class GetTasksByTagTest : IClassFixture<GetTasksByTagTestFixture>
{
    private readonly GetTasksByTagTestFixture _fixture;

    public GetTasksByTagTest(GetTasksByTagTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Handle should return tasks successfully")]
    public async Task Handle_Should_Return_Tasks_Successfully()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTasksFound(input.idTag);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCountGreaterThan(0);
        result.All(t => t.Id > 0).Should().BeTrue();
    }

    [Fact(DisplayName = "Handle should return empty list when no tasks found")]
    public async Task Handle_Should_Return_Empty_List_When_No_Tasks_Found()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTasksNotFound(input.idTag);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}
