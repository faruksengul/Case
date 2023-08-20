using MediatR;
using SystemCase.Application.Dtos.UserDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Core.Pagination;

namespace SystemCase.Application.Cqrs.Queries.UserQueries;

public class GetUserAllQuery : PaginationParams, IRequest<ApiResponse<List<UserDto>>>
{
    public string? Name { get; set; }

    public string? Surname { get; set; }

    public string? Email { get; set; }

    public bool? IsActive { get; set; }
}