using System.Net;
using FluentValidation;
using MediatR;
using SystemCase.Application.Cqrs.Queries.UserQueries;
using SystemCase.Application.Dtos.UserDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.Data.Context;
using SystemCase.Infrastructure.UnitOfWork;

namespace SystemCase.Application.Cqrs.QueryHandlers.UserQueryHandlers;

public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserDto>>
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;

    public GetUserByIdQueryHandler(IUnitOfWork<AppDbContext> unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<ApiResponse<UserDto>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var existRecord = await _unitOfWork.GetRepository<User>().GetByIdWithProjectToAsync<UserDto>(request.Id, cancellationToken);

        if (existRecord is null)
            throw new ValidationException("User is not found.");

        return new ApiResponse<UserDto>
        {
            Data = existRecord,
            StatusCode = (int)HttpStatusCode.OK,
            IsSuccessful = true,
            TotalItemCount = 1
        };
    }
}