using FluentAssertions;

namespace UnitTests.TaskUseCasesTest;

public class AddTagToTaskTest : IClassFixture<AddTagToTaskTestFixture>
{
    private readonly AddTagToTaskTestFixture _fixture;

    public AddTagToTaskTest(AddTagToTaskTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Handle should add tag successfully")]
    public async Task Handle_Should_Add_Tag_Successfully()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTaskFound(input.Id);
        _fixture.SetupTagFound(input.IdTag);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Tag.Should().NotBeNull();
        result.Tag.Id.Should().Be(input.IdTag);
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
