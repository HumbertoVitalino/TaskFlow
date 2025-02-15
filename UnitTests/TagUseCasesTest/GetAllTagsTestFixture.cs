using AutoBogus;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.TagUseCases.GetAllTags;
using Core.UseCases.TagUseCases.GetAllTags.Boundaries;
using Core.UseCases.TagUseCases.Output;
using Moq;

namespace UnitTests.TagUseCasesTest;

public class GetAllTagsTestFixture
{
    private readonly Mock<ITagRepository> _tagRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly GetAllTags _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public GetAllTagsTestFixture()
    {
        _useCase = SetupGetAllTagsUseCase();
    }

    private GetAllTags SetupGetAllTagsUseCase()
    {
        _mapper
            .Setup(m => m.Map<List<TagOutput>>(It.IsAny<List<Tag>>()))
            .Returns((List<Tag> tags) => tags.Select(tag => new TagOutput
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description
            }).ToList());

        return new GetAllTags(_tagRepository.Object, _mapper.Object);
    }

    public async Task<List<TagOutput>> HandleUseCaseAsync(GetAllTagsInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public void SetupTagsFound()
    {
        var tags = new AutoFaker<Tag>()
            .RuleFor(t => t.Id, f => f.Random.Int(1, 1000))
            .Generate(3);

        _tagRepository
            .Setup(repo => repo.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(tags);
    }

    public void SetupTagsNotFound()
    {
        _tagRepository
            .Setup(repo => repo.GetAll(It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Tag>());
    }

    public GetAllTagsInput SetupValidInput() => new();
}
