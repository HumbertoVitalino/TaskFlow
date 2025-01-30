using Core.UseCases.UserUseCases.Output;
using MediatR;

namespace Core.UseCases.UserUseCases.CreateUser.Boundaries;

public sealed record CreateUserInput(string Name, string Email, string Password, string Confirmation) : IRequest<UserOutput>;
