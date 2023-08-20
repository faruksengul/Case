using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SystemCase.Application.Cqrs.Queries.UserQueries;
using SystemCase.Application.Dtos.UserDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Core.Pagination;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.Data.Context;
using SystemCase.Infrastructure.UnitOfWork;

namespace SystemCase.Application.Cqrs.QueryHandlers.UserQueryHandlers;

public class GetUserAllQueryHandler : IRequestHandler<GetUserAllQuery, ApiResponse<List<UserDto>>>
{
    private readonly IUnitOfWork<AppDbContext> _unitOfWork;
    private readonly IMapper _mapper;

    public GetUserAllQueryHandler(IUnitOfWork<AppDbContext> unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<UserDto>>> Handle(GetUserAllQuery request, CancellationToken cancellationToken)
    {
        var resRepo = _unitOfWork.GetRepository<User>()
            .GetAll(new PaginationParams
            {
                PageId = request.PageId,
                PageSize = request.PageSize,
                OrderKey = request.OrderKey,
                OrderType = request.OrderType
            });

        IQueryable<User> filteredData = ApplyFilter(resRepo.Item1, request.Name, request.Surname, request.Email, request.IsActive);

        var data = await filteredData
            .ProjectTo<UserDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return new ApiResponse<List<UserDto>>
        {
            Data = data,
            StatusCode = (int)HttpStatusCode.OK,
            IsSuccessful = true,
            TotalItemCount = resRepo.Item2
        };
    }

    private IQueryable<User> ApplyFilter(IQueryable<User> source, string? name, string? surname, string? email, bool? isActive)
    {
        if (!string.IsNullOrEmpty(name))
            source = source.Where(x => x.Name.Contains(name));

        if (!string.IsNullOrEmpty(surname))
            source = source.Where(x => x.Surname.Contains(surname));

        if (!string.IsNullOrEmpty(email))
            source = source.Where(x => x.Email.Contains(email));

        if (isActive is not null)
            source = source.Where(x => x.IsActive.Equals(isActive));

        return source;
    }
}
