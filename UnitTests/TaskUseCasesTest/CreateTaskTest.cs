using FluentAssertions;

namespace UnitTests.TaskUseCasesTest;

public class CreateTaskTest : IClassFixture<CreateTaskTestFixture>
{
    private readonly CreateTaskTestFixture _fixture;

    public CreateTaskTest(CreateTaskTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Handle should act with success")]
    public void Handle_Should_Act_With_Success()
    {
        // Arrange
        var input = _fixture.SetupValidInput();

        // Act
        var result = _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
    }

    [Fact(DisplayName = "Handle should act with success without tagId")]
    public void Handle_Should_Act_With_Success_Without_TagId()
    {
        // Arrange
        var input = _fixture.SetupValidInputWithoutTag();

        // Act
        var result = _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
    }
    
}
