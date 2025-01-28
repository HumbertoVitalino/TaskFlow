using AutoMapper;
using Core.Entities;
using Core.UseCases.UserUseCases.CreateUser.Boundaries;
using Core.UseCases.UserUseCases.Output;

namespace Core.UseCases.UserUseCases.CreateUser.Mapper;

public sealed class CreateUserMapper : Profile
{
    public CreateUserMapper()
    {
        CreateMap<CreateUserInput, User>();
        CreateMap<User, UserOutput>();
    }
}
