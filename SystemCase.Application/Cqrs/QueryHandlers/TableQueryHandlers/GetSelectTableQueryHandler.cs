using MediatR;
using SystemCase.Application.Cqrs.Queries.TableQueries;
using SystemCase.Application.Dtos.TableDtos;

namespace SystemCase.Application.Cqrs.QueryHandlers.TableQueryHandlers;

public class GetSelectTableQueryHandler : IRequestHandler<GetSelectTableQuery, List<TableDto>>
{
    public Task<List<TableDto>> Handle(GetSelectTableQuery request, CancellationToken cancellationToken)
    {
        var selectedTables = new List<TableDto>();
        var numberOfperson = request.NumberOfPerson;

        var sortedTables = request.AvailableTable.OrderBy(t => t.Capacity).ToList();

        foreach (var table in sortedTables)
        {
            if (numberOfperson <= 0)
            {
                break;
            }

            if (table.Capacity <= numberOfperson)
            {
                selectedTables.Add(table);
                numberOfperson -= table.Capacity;
            }
        }
        // Eğer tüm masaları doldurduktan sonra hala kişi kaldıysa, büyük masaları tekrar kontrol edelim.
        if (numberOfperson > 0)
        {
            foreach (var table in sortedTables.OrderByDescending(t => t.Capacity))
            {
                if (numberOfperson <= 0)
                {
                    break;
                }

                if (!selectedTables.Contains(table) && table.Capacity >= numberOfperson)
                {
                    selectedTables.Add(table);
                    numberOfperson -= table.Capacity;
                }
            }
        }

        return Task.FromResult(selectedTables);
    }
}