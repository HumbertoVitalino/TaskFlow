using Core.UseCases.UserUseCases.Output;
using MediatR;

namespace Core.UseCases.UserUseCases.LoginUser.Boundaries;

public sealed record LoginUserInput(string Email, string Password) : IRequest<UserOutput>;
