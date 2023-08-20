using MediatR;
using SystemCase.Application.Dtos.UserDtos;
using SystemCase.Application.Wrappers;

namespace SystemCase.Application.Cqrs.Queries.UserQueries;

public class GetUserByEmailQuery : IRequest<ApiResponse<UserDto>>
{
    public string Email { get; private set; }

    public GetUserByEmailQuery(string email)
    {
        Email = email;
    }
}