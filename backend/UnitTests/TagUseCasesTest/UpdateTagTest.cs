using FluentAssertions;

namespace UnitTests.TagUseCasesTest;

public class UpdateTagTest : IClassFixture<UpdateTagTestFixture>
{
    private readonly UpdateTagTestFixture _fixture;

    public UpdateTagTest(UpdateTagTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Handle should update tag successfully")]
    public async Task Handle_Should_Update_Tag_Successfully()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTagFound(input.Id);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(input.Id);
        result.Name.Should().Be(input.Name);
        result.Description.Should().Be(input.Description);

        _fixture.VerifyTagUpdated(input.Id);
        _fixture.VerifyUnitOfWorkCommitted();
    }

    [Fact(DisplayName = "Handle should return null when tag is not found")]
    public async Task Handle_Should_Return_Null_When_Tag_Not_Found()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTagNotFound(input.Id);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().BeNull();
    }
}
