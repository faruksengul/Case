using MediatR;
using SystemCase.Application.Dtos.AuthDtos;
using SystemCase.Application.Wrappers;

namespace SystemCase.Application.Cqrs.Queries.AuthQueries;

public class CreateTokenQuery : IRequest<ApiResponse<TokenDto>>
{
    public string Email { get; set; }

    public string Password { get; set; }
}