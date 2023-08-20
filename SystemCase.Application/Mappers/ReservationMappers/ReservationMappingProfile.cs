using AutoMapper;
using SystemCase.Application.Cqrs.Commands.CustomerCommands;
using SystemCase.Application.Cqrs.Commands.ReservationCommands;
using SystemCase.Application.Dtos.ReservationDtos;
using SystemCase.Domain.Entities;

namespace SystemCase.Application.Mappers.ReservationMappers;

public class ReservationMappingProfile:Profile
{
    public ReservationMappingProfile()
    {
        CreateMap<Reservation, ReservationDto>().ReverseMap();
        CreateMap<CreateReservationCommand, Reservation>().ReverseMap();
        CreateMap<CreateCustomerCommand, ReservationDto>();
    }
}