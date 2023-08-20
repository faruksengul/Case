using MediatR;
using SystemCase.Application.Dtos.ResponseDtos;
using SystemCase.Application.Wrappers;

namespace SystemCase.Application.Cqrs.Commands.UserCommands;

public class ChangePasswordUserCommand : IRequest<ApiResponse<NoDataDto>>
{
    public string OldPassword { get; set; }

    public string NewPassword { get; set; }

    public ChangePasswordUserCommand(string oldPassword, string newPassword)
    {
        OldPassword = oldPassword;
        NewPassword = newPassword;
    }
}