using System.Net;
using FluentValidation;
using MediatR;
using SystemCase.Application.Cqrs.Commands.UserCommands;
using SystemCase.Application.Dtos.ResponseDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.UnitOfWork;

namespace SystemCase.Application.Cqrs.CommandHandlers.UserCommandHandlers;

public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, ApiResponse<NoDataDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<NoDataDto>> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var existUser = await _unitOfWork.GetRepository<User>().GetByIdAsync(request.Id, cancellationToken);

        if (existUser is null)
            throw new ValidationException("User is not found.");

        _unitOfWork.GetRepository<User>().Remove(existUser);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<NoDataDto>
        {
            StatusCode = (int)HttpStatusCode.NoContent,
            IsSuccessful = true,
        };
    }
}