using AutoMapper;
using SystemCase.Application.Cqrs.Commands.CustomerCommands;
using SystemCase.Application.Cqrs.Commands.TableCommands;
using SystemCase.Application.Dtos.TableDtos;
using SystemCase.Domain.Entities;

namespace SystemCase.Application.Mappers.TableMappers;

public class TableMappingProfile:Profile
{
    public TableMappingProfile()
    {
        CreateMap<Table, TableDto>().ReverseMap();
        CreateMap<CreateTableCommand, Table>().ReverseMap();
        CreateMap<CreateCustomerCommand, TableDto>();
    }
}