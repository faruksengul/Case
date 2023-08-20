using AutoMapper;
using MediatR;
using SystemCase.Application.Cqrs.Queries.TableQueries;
using SystemCase.Application.Dtos.TableDtos;
using SystemCase.Domain.Entities;
using SystemCase.Infrastructure.UnitOfWork;

namespace SystemCase.Application.Cqrs.QueryHandlers.TableQueryHandlers;

public class GetAvailableTableQueryHandler:IRequestHandler<GetAvailableTableQuery,List<TableDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAvailableTableQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public Task<List<TableDto>> Handle(GetAvailableTableQuery request, CancellationToken cancellationToken)
    {
        var availableTablesToday = _unitOfWork.GetRepository<Table>()
            .Where(f => !f.Reservations.Any(r => r.ReservationDate.Date == request.Date))
            .OrderBy(f => f.Capacity)
            .ToList();
        return  Task.FromResult(_mapper.Map<List<TableDto>>(availableTablesToday));
    }
}