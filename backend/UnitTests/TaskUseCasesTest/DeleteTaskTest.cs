using Core.UseCases.TaskUseCases.Output;
using FluentAssertions;

namespace UnitTests.TaskUseCasesTest;

public class DeleteTaskTest : IClassFixture<DeleteTaskTestFixture>
{
    private readonly DeleteTaskTestFixture _fixture;

    public DeleteTaskTest(DeleteTaskTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Handle should act with success")]
    public async Task Handle_Should_Act_With_Success()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTaskExists();

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(input.Id);
        result.Title.Should().NotBeNullOrWhiteSpace();
        result.Description.Should().NotBeNullOrWhiteSpace();
    }

    [Fact(DisplayName = "Handle should return default when task is not found")]
    public async Task Handle_Should_Return_Default_When_Task_Not_Found()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTaskNotFound();

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().BeNull();
    }
}
