using FluentAssertions;

namespace UnitTests.TaskUseCasesTest;

public class GetAllTasksTest : IClassFixture<GetAllTasksTestFixture>
{
    private readonly GetAllTasksTestFixture _fixture;

    public GetAllTasksTest(GetAllTasksTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Handle should return tasks successfully")]
    public async Task Handle_Should_Return_Tasks_Successfully()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTasksFound(1);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().NotBeEmpty();
        result.Count.Should().Be(5);
    }

    [Fact(DisplayName = "Handle should return empty list when no tasks are found")]
    public async Task Handle_Should_Return_Empty_List_When_No_Tasks_Found()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTasksNotFound(2);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}
