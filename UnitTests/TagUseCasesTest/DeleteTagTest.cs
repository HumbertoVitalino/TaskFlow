using FluentAssertions;

namespace UnitTests.TagUseCasesTest;

public class DeleteTagTest : IClassFixture<DeleteTagTestFixture>
{
    private readonly DeleteTagTestFixture _fixture;

    public DeleteTagTest(DeleteTagTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Handle should delete tag successfully")]
    public async Task Handle_Should_Delete_Tag_Successfully()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTagFound(input.Id);

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(input.Id);

        _fixture.VerifyTagRepositoryDeleteCalled();
        _fixture.VerifyUnitOfWorkCommitCalled();
    }

    [Fact(DisplayName = "Handle should return null when tag not found")]
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
