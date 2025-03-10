using Core.UseCases.TaskUseCases.Output;
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
    public async void Handle_Should_Act_With_Success()
    {
        // Arrange
        var input = _fixture.SetupValidInput();

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().NotBe(0);
        result.Title.Should().Be(input.Title);
        result.Description.Should().Be(input.Description);
        result.Status.Should().Be(0);
        result.Tag.Should().NotBeNull();
        result.Tag.Id.Should().Be(input.TagId);
        result.Tag.Name.Should().Be($"Tag {input.TagId}");
    }

    [Fact(DisplayName = "Handle should act with success without tagId")]
    public async void Handle_Should_Act_With_Success_Without_TagId()
    {
        // Arrange
        var input = _fixture.SetupValidInputWithoutTag();

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Title.Should().Be(input.Title);
        result.Description.Should().Be(input.Description);
        result.Status.Should().Be(0);
        result.Tag.Should().BeNull();
    }

}
