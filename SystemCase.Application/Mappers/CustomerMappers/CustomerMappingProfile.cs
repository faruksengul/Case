using AutoMapper;
using SystemCase.Application.Cqrs.Commands.CustomerCommands;
using SystemCase.Application.Cqrs.Commands.ReservationCommands;
using SystemCase.Application.Dtos.CustomerDtos;
using SystemCase.Domain.Entities;

namespace SystemCase.Application.Mappers.CustomerMappers;

public class CustomerMappingProfile:Profile
{
    public CustomerMappingProfile()
    {
        CreateMap<Customer, CustomerDto>().ReverseMap();
        CreateMap<CreateCustomerCommand, CustomerDto>().ReverseMap();
        CreateMap<CreateCustomerCommand, Customer>().ReverseMap();
    }
}