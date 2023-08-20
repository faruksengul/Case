using System.Net;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SystemCase.Application.Cqrs.Queries.AuthQueries;
using SystemCase.Application.Dtos.AuthDtos;
using SystemCase.Application.Dtos.UserDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Core.Extensions;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.UnitOfWork;

namespace SystemCase.Application.Cqrs.QueryHandlers.AuthQueryHandlers;

public class CreateTokenQueryHandler : IRequestHandler<CreateTokenQuery, ApiResponse<TokenDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    public CreateTokenQueryHandler(IUnitOfWork unitOfWork, IMediator mediator, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mediator = mediator;
        _mapper = mapper;
    }

    public async Task<ApiResponse<TokenDto>> Handle(CreateTokenQuery request, CancellationToken cancellationToken)
    {
        var existUser = await _unitOfWork.GetRepository<User>()
            .Where(x => x.Email.Equals(request.Email))
            .SingleOrDefaultAsync(cancellationToken);

        if (existUser is null)
            throw new ValidationException("User is not found.");

        if (!existUser.IsActive)
            throw new ValidationException("The user is inactive in the system.");

        if (!HashingManager.VerifyHashedValue(existUser.Password, request.Password))
            throw new ValidationException("User is not found.");

        var userDto = _mapper.Map<UserDto>(existUser);

        var generatedToken = await _mediator.Send(new GenerateTokenQuery(userDto), cancellationToken);

        return new ApiResponse<TokenDto>
        {
            Data = generatedToken.Data,
            IsSuccessful = true,
            StatusCode = (int)HttpStatusCode.OK,
            TotalItemCount = 1
        };
    }
}