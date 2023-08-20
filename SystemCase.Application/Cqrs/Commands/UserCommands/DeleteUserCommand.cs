using MediatR;
using SystemCase.Application.Dtos.ResponseDtos;
using SystemCase.Application.Wrappers;

namespace SystemCase.Application.Cqrs.Commands.UserCommands;

public class DeleteUserCommand : IRequest<ApiResponse<NoDataDto>>
{
    public Guid Id { get; private set; }

    public DeleteUserCommand(Guid id)
    {
        Id = id;
    }
}
