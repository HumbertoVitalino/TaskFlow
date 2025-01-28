using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.UseCases.UserUseCases.CreateUser.Boundaries;
using Core.UseCases.UserUseCases.Output;
using MediatR;

namespace Core.UseCases.UserUseCases.CreateUser;

public class CreateUser : IRequestHandler<CreateUserInput, UserOutput>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public CreateUser(IUserRepository userRepository, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<UserOutput> Handle(CreateUserInput input, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<User>(input);

        _userRepository.Create(user);

        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<UserOutput>(user);
    }
}
