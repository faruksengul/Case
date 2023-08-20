using MediatR;
using SystemCase.Application.Dtos.TableDtos;
using SystemCase.Application.Wrappers;
using SystemCase.Domain.Enums;

namespace SystemCase.Application.Cqrs.Commands.TableCommands;

public class CreateTableCommand:IRequest<ApiResponse<TableDto>>
{
    public int Capacity { get; set; }
    public TableTypeEnum TableType { get; set; }
}