using MediatR;
using SystemCase.Application.Dtos.AuthDtos;
using SystemCase.Application.Dtos.UserDtos;
using SystemCase.Application.Wrappers;

namespace SystemCase.Application.Cqrs.Queries.AuthQueries;

internal class GenerateTokenQuery : IRequest<ApiResponse<TokenDto>>
{
    public UserDto User { get; set; }

    public GenerateTokenQuery(UserDto user)
    {
        User = user;
    }
}