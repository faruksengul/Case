using MediatR;
using SystemCase.Application.Dtos.TableDtos;

namespace SystemCase.Application.Cqrs.Queries.TableQueries;

public class GetAvailableTableQuery:IRequest<List<TableDto>>
{
    public DateTime Date { get;  set; }

    public GetAvailableTableQuery(DateTime date)
    {
        Date = date;
    }
}