using AutoBogus;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.TagUseCases.UpdateTag;
using Core.UseCases.TagUseCases.UpdateTag.Boundaries;
using Core.UseCases.TagUseCases.Output;
using Moq;

namespace UnitTests.TagUseCasesTest;

public class UpdateTagTestFixture
{
    private readonly Mock<ITagRepository> _tagRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly UpdateTag _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public UpdateTagTestFixture()
    {
        _useCase = SetupUpdateTagUseCase();
    }

    private UpdateTag SetupUpdateTagUseCase()
    {
        _unitOfWork
            .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
            .Returns(System.Threading.Tasks.Task.CompletedTask);

        _mapper
            .Setup(m => m.Map<TagOutput>(It.IsAny<Tag>()))
            .Returns((Tag tag) => new TagOutput
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description
            });

        return new UpdateTag(
            _tagRepository.Object,
            _mapper.Object,
            _unitOfWork.Object);
    }

    public async Task<TagOutput> HandleUseCaseAsync(UpdateTagInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public void SetupTagFound(int tagId)
    {
        var tag = new AutoFaker<Tag>()
            .RuleFor(t => t.Id, _ => tagId)
            .Generate();

        _tagRepository
            .Setup(repo => repo.Get(tagId, It.IsAny<CancellationToken>()))
            .ReturnsAsync(tag);
    }

    public void SetupTagNotFound(int tagId)
    {
        _tagRepository
            .Setup(repo => repo.Get(tagId, It.IsAny<CancellationToken>()))
            .ReturnsAsync((Tag)null!);
    }

    public void VerifyTagUpdated(int tagId)
    {
        _tagRepository.Verify(repo => repo.Update(It.Is<Tag>(t => t.Id == tagId)), Times.Once);
    }

    public void VerifyUnitOfWorkCommitted()
    {
        _unitOfWork.Verify(u => u.Commit(It.IsAny<CancellationToken>()), Times.Once);
    }

    public UpdateTagInput SetupValidInput()
        => new AutoFaker<UpdateTagInput>()
            .RuleFor(x => x.Id, f => f.Random.Int(1, 1000))
            .Generate();
}
