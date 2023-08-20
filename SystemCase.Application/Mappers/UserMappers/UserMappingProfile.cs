using AutoMapper;
using SystemCase.Application.Cqrs.Commands.UserCommands;
using SystemCase.Application.Cqrs.Queries.UserQueries;
using SystemCase.Application.Dtos.UserDtos;
using SystemCase.Domain.Entities;

namespace SystemCase.Application.Mappers.UserMappers;

public class UserMappingProfile:Profile
{
    public UserMappingProfile()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<CreateUserCommand, User>().ReverseMap();
        CreateMap<UpdateUserCommand, User>().ReverseMap();
        CreateMap<GetUserAllQuery, User>().ReverseMap();
    }
}