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

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using (var hmac = new System.Security.Cryptography.HMACSHA512())
        {
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }
    }

    public async Task<UserOutput> Handle(CreateUserInput input, CancellationToken cancellationToken)
    {
        var existingUser = await _userRepository.GetByEmailAsync(input.Email, cancellationToken);
        if (existingUser != null)
        {
            return new UserOutput
            {
                Data = string.Empty,
                Message = "This email is already in use;",
                Status = false
            };
        }

        if (input.Password != input.Confirmation)
        {
            return new UserOutput
            {
                Data = string.Empty,
                Message = "Confirm your password",
                Status = false
            };
        }

        CreatePasswordHash(input.Password, out byte[] passwordHash, out byte[] passwordSalt);

        var user = _mapper.Map<User>(input);
        user.PasswordHash = passwordHash;
        user.PasswordSalt = passwordSalt;

        _userRepository.Create(user);
        await _unitOfWork.Commit(cancellationToken);

        return _mapper.Map<UserOutput>(user);
    }   
}
