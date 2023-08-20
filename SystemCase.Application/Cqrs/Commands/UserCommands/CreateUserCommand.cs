using MediatR;
using SystemCase.Application.Dtos.UserDtos;
using SystemCase.Application.Wrappers;

namespace SystemCase.Application.Cqrs.Commands.UserCommands;

public class CreateUserCommand : IRequest<ApiResponse<UserDto>>
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }
}