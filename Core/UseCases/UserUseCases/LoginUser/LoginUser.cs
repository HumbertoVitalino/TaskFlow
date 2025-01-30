using AutoMapper;
using Core.Interfaces;
using Core.Services;
using Core.UseCases.UserUseCases.LoginUser.Boundaries;
using Core.UseCases.UserUseCases.Output;
using MediatR;

namespace Core.UseCases.UserUseCases.LoginUser;

public class LoginUser : IRequestHandler<LoginUserInput, UserOutput>
{
    private readonly IUserRepository _userRepository;
    private readonly JwtTokenService _jwtTokenService;

    public LoginUser(IUserRepository userRepository, JwtTokenService jwtTokenService)
    {
        _userRepository = userRepository;
        _jwtTokenService = jwtTokenService;
    }

    public async Task<UserOutput> Handle(LoginUserInput input, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(input.Email, cancellationToken);

        if (user == null)
            throw new UnauthorizedAccessException("Invalid email or password.");

        using (var hmac = new System.Security.Cryptography.HMACSHA512(user.PasswordSalt))
        {
            var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(input.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password");
        }

        var token = _jwtTokenService.GenerateToken(user.Email);

        var userOutput = new UserOutput
        {
            Data = $"User ID: {user.Id}, Name: {user.Name}, Email: {user.Email}",
            Message = "Login successful",
            Status = true,
            Token = token 
        };

        return userOutput;
    }
}
