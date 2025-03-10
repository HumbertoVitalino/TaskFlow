using AutoBogus;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.TagUseCases.DeleteTag;
using Core.UseCases.TagUseCases.DeleteTag.Boundaries;
using Core.UseCases.TagUseCases.Output;
using Moq;

namespace UnitTests.TagUseCasesTest;

public class DeleteTagTestFixture
{
    private readonly Mock<ITagRepository> _tagRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly DeleteTag _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public DeleteTagTestFixture()
    {
        _useCase = SetupDeleteTagUseCase();
    }

    private DeleteTag SetupDeleteTagUseCase()
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

        return new DeleteTag(_unitOfWork.Object, _mapper.Object, _tagRepository.Object);
    }

    public async Task<TagOutput> HandleUseCaseAsync(DeleteTagInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public DeleteTagInput SetupValidInput()
        => new AutoFaker<DeleteTagInput>().Generate();

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

    public void VerifyTagRepositoryDeleteCalled()
        => _tagRepository.Verify(repo => repo.Delete(It.IsAny<Tag>()), Times.Once);

    public void VerifyUnitOfWorkCommitCalled()
        => _unitOfWork.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.Once);
}
