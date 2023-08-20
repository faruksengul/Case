using MediatR;
using SystemCase.Application.Dtos.TableDtos;

namespace SystemCase.Application.Cqrs.Queries.TableQueries;

public class GetSelectTableQuery:IRequest<List<TableDto>>
{
    public GetSelectTableQuery(List<TableDto> availableTable, int numberOfPerson)
    {
        AvailableTable = availableTable;
        NumberOfPerson = numberOfPerson;
    }

    public int NumberOfPerson { get;  set; }
    public List<TableDto> AvailableTable { get; set; }
}