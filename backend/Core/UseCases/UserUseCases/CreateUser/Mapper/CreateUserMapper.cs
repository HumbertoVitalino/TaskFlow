using AutoMapper;
using Core.Entities;
using Core.UseCases.UserUseCases.CreateUser.Boundaries;
using Core.UseCases.UserUseCases.Output;

public sealed class CreateUserMapper : Profile
{
    public CreateUserMapper()
    {
        CreateMap<CreateUserInput, User>()
            .ForMember(dest => dest.PasswordHash, opt => opt.Ignore())
            .ForMember(dest => dest.PasswordSalt, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForSourceMember(src => src.Confirmation, opt => opt.DoNotValidate());

        CreateMap<User, UserOutput>();
    }
}
