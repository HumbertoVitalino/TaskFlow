using FluentAssertions;

namespace UnitTests.TagUseCasesTest;

public class CreateTagTest : IClassFixture<CreateTagTestFixture>
{
    private readonly CreateTagTestFixture _fixture;

    public CreateTagTest(CreateTagTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Handle should create tag successfully")]
    public async Task Handle_Should_Create_Tag_Successfully()
    {
        // Arrange
        var input = _fixture.SetupValidInput();

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().BeGreaterThan(0);
        result.Name.Should().Be(input.Name);
        result.Description.Should().Be(input.Description);
    }

    [Fact(DisplayName = "Handle should call repository and commit changes")]
    public async Task Handle_Should_Call_Repository_And_Commit_Changes()
    {
        // Arrange
        var input = _fixture.SetupValidInput();

        // Act
        await _fixture.HandleUseCaseAsync(input);

        // Assert
        _fixture.VerifyTagRepositoryCreateCalled();
        _fixture.VerifyUnitOfWorkCommitCalled();
    }
}
