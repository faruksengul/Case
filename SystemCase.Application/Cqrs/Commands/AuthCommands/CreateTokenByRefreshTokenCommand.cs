using MediatR;
using SystemCase.Application.Dtos.AuthDtos;
using SystemCase.Application.Wrappers;

namespace SystemCase.Application.Cqrs.Commands.AuthCommands;

public class CreateTokenByRefreshTokenCommand : IRequest<ApiResponse<TokenDto>>
{
    public string RefreshToken { get; set; }

    public CreateTokenByRefreshTokenCommand(string refreshToken)
    {
        RefreshToken = refreshToken;
    }
}