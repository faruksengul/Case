using System.Net;
using System.Security.Claims;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using SystemCase.Application.Cqrs.Commands.UserCommands;
using SystemCase.Application.Dtos.ResponseDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Core.Extensions;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.UnitOfWork;

namespace SystemCase.Application.Cqrs.CommandHandlers.UserCommandHandlers;

public class ChangePasswordUserCommandHandler : IRequestHandler<ChangePasswordUserCommand, ApiResponse<NoDataDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ChangePasswordUserCommandHandler(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    {
        _unitOfWork = unitOfWork;
        _httpContextAccessor = httpContextAccessor;
    }

    public async Task<ApiResponse<NoDataDto>> Handle(ChangePasswordUserCommand request, CancellationToken cancellationToken)
    {
        Guid userId = Guid.Parse(_httpContextAccessor.HttpContext.User.Claims.First(x => x.Type.Equals(ClaimTypes.NameIdentifier)).Value);

        var existUser = await _unitOfWork.GetRepository<User>()
            .Where(user => user.Id.Equals(userId) && user.IsActive)
            .SingleOrDefaultAsync(cancellationToken);

        if (existUser is null)
            throw new ValidationException("User email or password wrong.");

        if (!HashingManager.VerifyHashedValue(existUser.Password, request.OldPassword))
            throw new ValidationException("User email or password wrong.");

        var hashedNewPassword = HashingManager.HashValue(request.NewPassword);

        existUser.Password = hashedNewPassword;
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ApiResponse<NoDataDto>
        {
            StatusCode = (int)HttpStatusCode.NoContent
        };
    }
}