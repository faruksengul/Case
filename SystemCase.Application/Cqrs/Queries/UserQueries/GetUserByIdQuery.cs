using MediatR;
using SystemCase.Application.Dtos.UserDtos;
using SystemCase.Application.Wrappers;

namespace SystemCase.Application.Cqrs.Queries.UserQueries;

public class GetUserByIdQuery : IRequest<ApiResponse<UserDto>>
{
    public Guid Id { get; private set; }

    public GetUserByIdQuery(Guid id)
    {
        Id = id;
    }
}