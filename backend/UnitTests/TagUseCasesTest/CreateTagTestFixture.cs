using AutoBogus;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.TagUseCases.CreateTag;
using Core.UseCases.TagUseCases.CreateTag.Boundaries;
using Core.UseCases.TagUseCases.Output;
using Moq;

namespace UnitTests.TagUseCasesTest;

public class CreateTagTestFixture
{
    private readonly Mock<ITagRepository> _tagRepository = new();
    private readonly Mock<IMapper> _mapper = new();
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly CreateTag _useCase;

    public CancellationToken CancellationToken = CancellationToken.None;

    public CreateTagTestFixture()
    {
        _useCase = SetupCreateTagUseCase();
    }

    private CreateTag SetupCreateTagUseCase()
    {
        _unitOfWork
            .Setup(u => u.Commit(It.IsAny<CancellationToken>()))
            .Returns(System.Threading.Tasks.Task.CompletedTask);

        _mapper
            .Setup(m => m.Map<Tag>(It.IsAny<CreateTagInput>()))
            .Returns((CreateTagInput input) => new Tag
            {
                Id = new Random().Next(1, 1000),
                Name = input.Name,
                Description = input.Description
            });

        _mapper
            .Setup(m => m.Map<TagOutput>(It.IsAny<Tag>()))
            .Returns((Tag tag) => new TagOutput
            {
                Id = tag.Id,
                Name = tag.Name,
                Description = tag.Description
            });

        return new CreateTag(_unitOfWork.Object, _mapper.Object, _tagRepository.Object);
    }

    public async Task<TagOutput> HandleUseCaseAsync(CreateTagInput input) =>
        await _useCase.Handle(input, CancellationToken);

    public CreateTagInput SetupValidInput()
        => new AutoFaker<CreateTagInput>().Generate();
    
    public void VerifyTagRepositoryCreateCalled() =>
        _tagRepository.Verify(repo => repo.Create(It.IsAny<Tag>()), Times.AtLeastOnce);

    public void VerifyUnitOfWorkCommitCalled() =>
        _unitOfWork.Verify(uow => uow.Commit(It.IsAny<CancellationToken>()), Times.AtLeastOnce);
}
