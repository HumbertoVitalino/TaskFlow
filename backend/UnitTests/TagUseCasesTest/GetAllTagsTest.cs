using FluentAssertions;

namespace UnitTests.TagUseCasesTest;

public class GetAllTagsTest : IClassFixture<GetAllTagsTestFixture>
{
    private readonly GetAllTagsTestFixture _fixture;

    public GetAllTagsTest(GetAllTagsTestFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact(DisplayName = "Handle should return list of tags successfully")]
    public async Task Handle_Should_Return_List_Of_Tags_Successfully()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTagsFound();

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(3);
    }

    [Fact(DisplayName = "Handle should return empty list when no tags are found")]
    public async Task Handle_Should_Return_Empty_List_When_No_Tags_Are_Found()
    {
        // Arrange
        var input = _fixture.SetupValidInput();
        _fixture.SetupTagsNotFound();

        // Act
        var result = await _fixture.HandleUseCaseAsync(input);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }
}
