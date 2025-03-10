using FluentAssertions;

namespace UnitTests.SubTaskUseCasesTest;

public class DeleteSubTaskTest
{
    private readonly DeleteSubTaskTestFixture _fixture = new();

    [Fact(DisplayName = "Handle Should Delete SubTask When SubTask Exists")]
    public async Task Handle_ShouldDeleteSubTask_WhenSubTaskExists()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupSubTaskFound(input.Id);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(input.Id);
    }

    [Fact(DisplayName = "Handle Should Return Default When SubTask Does Not Exist")]
    public async Task Handle_ShouldReturnDefault_WhenSubTaskDoesNotExist()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupSubTaskNotFound(input.Id);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().BeNull();
    }
}
